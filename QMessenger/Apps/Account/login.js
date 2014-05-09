angular.module('loginApp', [])
.controller('loginController', function ($scope, $http) {
    $scope.account = {
        emailId: "",
        passwordHash: ""
    };

    function isValidEmail(maybeEmail) {
        var emailRegex = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        return emailRegex.test(maybeEmail);
    }

    function isValidPassword(password) {
        if (password.length < 6) {
            return { result: false, message: '패스워드가 너무 짧습니다' };
        }
        return { result: true };
    }

    function hashPassword(password) {
        return password;
    }

    $scope.loginAjax = function () {
        var accountInfo = $scope.account;

        if (!isValidEmail(accountInfo.emailId)) {
            $scope.alert = '메일 주소가 올바르지 않습니다';
            return;
        }

        var passwordValidation = isValidPassword(accountInfo.passwordHash);
        if (passwordValidation.result !== true) {
            $scope.alert = passwordValidation.message;
            return;
        }

        $http({
            url: '/Account/LoginAjax',
            method: 'POST',
            data: accountInfo,
            headers: {
                'Content-Type': 'application/json'
            }
        }).success(function (data, status, headers, config) {
            if (data === true) {
                alert('true');
            }
            else if (data === false) {
                alert('false');
            }
            else {
                alert('other');
            }
        }).error(function (data, status, headers, config) {
            alert(status + ' ' + headers);
        })
    };
});