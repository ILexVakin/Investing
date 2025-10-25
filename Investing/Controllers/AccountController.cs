using AutoMapper;
using Investing.Data;
using Investing.Extensions;
using Investing.Models;
using Investing.Models.DTO;
using Investing.Models.ViewModels;
using Investing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class AccountController : Controller
    {
        private readonly MainContext _mainContext;
        private readonly IMapper _mapper;
        private const string RememberMeCookieName = "RememberMe";
        private readonly ILogger<HomeController> _logger;
        public AccountController(MainContext mainContext,
            IMapper mapper,
            ILogger<HomeController> logger)
        {
            _mainContext = mainContext;
            _mapper = mapper;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult LoginAccount()
        {
            // Проверяем, есть ли сохраненные данные в куках
            if (Request.Cookies.ContainsKey(RememberMeCookieName))
            {
                var cookieValue = Request.Cookies[RememberMeCookieName];
                string? [] credentials = cookieValue.Split(':');
                var user = new Credentials
                {
                    Login = credentials[0],
                    Password = credentials[1],
                };
                return View(user);
            }
            return View();
        }


        [HttpPost]
        public IActionResult LoginAccount(Credentials user)
        {
            if (ModelState.IsValid)
            {
                var foundUser = _mainContext.User.Include(c => c.Credentials)
                                                .Include(c => c.UserRole)
                                                .FirstOrDefault(c => c.Credentials.Login == user.Login &&
                                                c.Credentials.Password == user.Password);
                if (foundUser != null)
                {
                    if (user.IsRememberMe)
                    {
                        var cookieValue = $"{user.Login}:{user.Password}";
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                            HttpOnly = true,
                            Secure = true 
                        };
                        Response.Cookies.Append(RememberMeCookieName, cookieValue, cookieOptions);
                    }
                    else
                    {
                        // Удаляем куки, если "Запомнить меня" не выбрано
                        Response.Cookies.Delete(RememberMeCookieName);
                    }
                    
                    HttpContext.Session.SetUser(user);

                    _logger.LogInformation($"Пользователь {user.UserId} вошел в личный кабинет");
                    return RedirectToAction("PersonalAccount", new { userId = foundUser.UserId });
                }
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult PersonalAccount(long userId)
        {
            var user = _mainContext.User
            .Include(u => u.Credentials) // Подгружаем связанные данные
            .FirstOrDefault(u => u.UserId == userId);

            if (userId == null || userId == 0)
                return NotFound();

            var userCredentialsDTO = _mapper.Map<UserCredentialsDTO>(user);
            return View(userCredentialsDTO);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete(RememberMeCookieName);
            HttpContext.Session.Logout();
            return RedirectToAction("LoginAccount");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string mailRegex = @"^.*@(mail\.ru|gmail\.com)$";
            Regex regex = new Regex(mailRegex);

            if (regex.IsMatch(model.Login))
            {
                var existsUser = _mainContext.Credentials.FirstOrDefault(c => c.Login == model.Login);
                if (existsUser == null)
                {
                    using (var transaction = await _mainContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                            var user = new User
                            {
                                FirstName = model.FirstName,
                                Name = model.Name,
                                Age = model.Age
                            };
                            _mainContext.User.Add(user);
                            await _mainContext.SaveChangesAsync();

                            var credentials = new Credentials
                            {
                                Login = model.Login,
                                Password = passwordHash,
                                IsRememberMe = false,
                                CreatedAt = DateTime.Now
                            };
                            _mainContext.Credentials.Add(credentials);
                            await _mainContext.SaveChangesAsync();
                            await transaction.CommitAsync(cancellationToken);
                            return Ok(new { message = "Пользователь успешно зарегистрирован" });
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync(cancellationToken);
                            ModelState.AddModelError(string.Empty, "Произошла ошибка при регистрации. Пожалуйста, попробуйте еще раз.");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Пользователь с таким логином уже существует.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Login", "Логин должен соответствовать почте.");
                return View(model);
            }
        }
        public IActionResult OpenInvestingAccount()
        {
            return View();
        }

      
    }
}
