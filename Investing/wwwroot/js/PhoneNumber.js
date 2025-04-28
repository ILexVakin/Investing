var element = document.getElementById('phone');
var maskOptions = {
    mask: '+7(000)000-00-00',
    lazy: false
}
var mask = new IMask(element, maskOptions);

var element2 = document.getElementById('email');
var maskOptions2 = {
    mask: function (value) {
        if (/^[a-z0-9_\.-]+$/.test(value))
            return true;
        if (/^[a-z0-9_\.-]+@$/.test(value))
            return true;
        if (/^[a-z0-9_\.-]+@[a-z0-9-]+$/.test(value))
            return true;
        if (/^[a-z0-9_\.-]+@[a-z0-9-]+\.$/.test(value))
            return true;
        if (/^[a-z0-9_\.-]+@[a-z0-9-]+\.[a-z]{1,4}$/.test(value))
            return true;
        if (/^[a-z0-9_\.-]+@[a-z0-9-]+\.[a-z]{1,4}\.$/.test(value))
            return true;
        if (/^[a-z0-9_\.-]+@[a-z0-9-]+\.[a-z]{1,4}\.[a-z]{1,4}$/.test(value))
            return true;
        return false;
    },
    lazy: false
}
var mask2 = new IMask(element2, maskOptions2);

var element3 = document.getElementById('card');
var maskOptions3 = {
    mask: '0000 0000 0000 0000',
    lazy: false
}
var mask3 = new IMask(element3, maskOptions3);