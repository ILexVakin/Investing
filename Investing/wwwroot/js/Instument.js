class TodoSPA {
    constructor() {
        this.apiUrl = 'instrument/getlistinstruments';
        this.init();
    }

    init() {
        this.loadTodos(); // Автоматически загружаем задачи при старте
        this.setupAutoRefresh(); // Автообновление каждые 30 секунд
    }

    async loadTodos() {
        this.showLoading(true);
        this.hideError();

        try {
            const response = await fetch(this.apiUrl);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const todos = await response.json();
            console.log('✅ Задачи загружены:', todos);

            this.renderTodos(todos);
            this.updateStats(todos);

        } catch (error) {
            console.error('❌ Ошибка загрузки задач:', error);
            this.showError(`Не удалось загрузить задачи: ${error.message}`);
        } finally {
            this.showLoading(false);
        }
    }
    // Рендеринг списка задач
    renderTodos(todos) {
        const todoList = document.getElementById('todoList');

        if (todos.length === 0) {
            todoList.innerHTML = '<li style="text-align: center; color: #999;">🎉 Нет задач! Добавьте первую задачу.</li>';
            return;
        }

        todoList.innerHTML = todos.map(todo => `
            <li class="todo-item ${todo.isCompleted ? 'completed' : ''}" data-todo-id="${todo.id}">
                <div class="todo-content">
                    <span class="todo-title">${this.escapeHtml(todo.title)}</span>
                    ${todo.description ? `<br><small class="todo-description">${this.escapeHtml(todo.description)}</small>` : ''}
                    <div class="todo-meta">
                        <small>Создано: ${new Date(todo.createdAt).toLocaleDateString()}</small>
                    </div>
                </div>
                <div class="actions">
                    <button onclick="todoApp.toggleTodo(${todo.id}, ${todo.isCompleted})" 
                            title="${todo.isCompleted ? 'Вернуть в работу' : 'Отметить выполненной'}">
                        ${todo.isCompleted ? '↶' : '✓'}
                    </button>
                    <button onclick="todoApp.deleteTodo(${todo.id})" title="Удалить">🗑️</button>
                </div>
            </li>
        `).join('');
    }
    setupAutoRefresh() {
        // Автообновление каждые 30 секунд
        setInterval(() => {
            if (!document.hidden) { // Обновляем только если страница видима
                this.loadTodos();
            }
        }, 5000);
    }