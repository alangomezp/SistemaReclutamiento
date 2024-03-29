﻿var reclutamientoModule = angular.module('reclutamientoModule', ['ngRoute']);

reclutamientoModule.directive('checklistModel', ['$parse', '$compile', function ($parse, $compile) {
    // contains
    function contains(arr, item, comparator) {
        if (angular.isArray(arr)) {
            for (var i = arr.length; i--;) {
                if (comparator(arr[i], item)) {
                    return true;
                }
            }
        }
        return false;
    }

    // add
    function add(arr, item, comparator) {
        arr = angular.isArray(arr) ? arr : [];
        if (!contains(arr, item, comparator)) {
            arr.push(item);
        }
        return arr;
    }

    // remove
    function remove(arr, item, comparator) {
        if (angular.isArray(arr)) {
            for (var i = arr.length; i--;) {
                if (comparator(arr[i], item)) {
                    arr.splice(i, 1);
                    break;
                }
            }
        }
        return arr;
    }

    // http://stackoverflow.com/a/19228302/1458162
    function postLinkFn(scope, elem, attrs) {
        // compile with `ng-model` pointing to `checked`
        $compile(elem)(scope);

        // getter / setter for original model
        var getter = $parse(attrs.checklistModel);
        var setter = getter.assign;
        var checklistChange = $parse(attrs.checklistChange);

        // value added to list
        var value = $parse(attrs.checklistValue)(scope.$parent);


        var comparator = angular.equals;

        if (attrs.hasOwnProperty('checklistComparator')) {
            comparator = $parse(attrs.checklistComparator)(scope.$parent);
        }

        // watch UI checked change
        scope.$watch('checked', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }
            var current = getter(scope.$parent);
            if (newValue === true) {
                setter(scope.$parent, add(current, value, comparator));
            } else {
                setter(scope.$parent, remove(current, value, comparator));
            }

            if (checklistChange) {
                checklistChange(scope);
            }
        });

        // declare one function to be used for both $watch functions
        function setChecked(newArr, oldArr) {
            scope.checked = contains(newArr, value, comparator);
        }

        // watch original model change
        // use the faster $watchCollection method if it's available
        if (angular.isFunction(scope.$parent.$watchCollection)) {
            scope.$parent.$watchCollection(attrs.checklistModel, setChecked);
        } else {
            scope.$parent.$watch(attrs.checklistModel, setChecked, true);
        }
    }

    return {
        restrict: 'A',
        priority: 1000,
        terminal: true,
        scope: true,
        compile: function (tElement, tAttrs) {
            if (tElement[0].tagName !== 'INPUT' || tAttrs.type !== 'checkbox') {
                throw 'checklist-model should be applied to `input[type="checkbox"]`.';
            }

            if (!tAttrs.checklistValue) {
                throw 'You should provide `checklist-value`.';
            }

            // exclude recursion
            tElement.removeAttr('checklist-model');

            // local scope var storing individual checkbox model
            tElement.attr('ng-model', 'checked');

            return postLinkFn;
        }
    };
}]);


//Controlador de la pagina Layout
reclutamientoModule.controller('LayoutController', function ($scope, $http) {

    $scope.Desloguearse = function () {

        Swal.fire({
            title: '¿Está seguro de cerrar sesión?',
            text: "Si es asi, esperamos que vuela pronto :)",
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Cerrar Sesión!'
        }).then((result) => {
            if (result.value) {
                window.location.href = "/Home/Index";
            }
        })

    }


})


//Controlador de la pagina Index donde esta el Login
reclutamientoModule.controller('LoginController', function ($scope, $http) {

    $scope.usuario = '';
    $scope.password = '';
    $scope.error = false;
    $scope.isLoading = false;

    $scope.Login = function () {

        $scope.isLoading = true;
        $scope.error = false;
        var datos = {

            usuario: $scope.usuario,
            password: $scope.password

        }

        $http.post('/Login/LoginUsers', datos)
            .then(function success(data) {

                $scope.isLoading = false;

                if (data.data == true) {
                    $scope.error = true;
                } else {
                    window.location.href = data.data;
                }
                
            }, function error() {
                    $scope.isLoading = false;
                console.log("error");
            })
    }
});

//Controlador de la pagina CrearUsuarios
reclutamientoModule.controller('CrearUsuarioController', function ($http, $scope) {

    $scope.nombreUsuario = '';
    $scope.pass = '';
    $scope.correo = '';
    $scope.confirmPass = '';
    $scope.Perfiles = {};
    $scope.Usuarios = [];
    var usuarioAEditar = {};
    $scope.modify = false;
    $scope.isLoading = false;


    GetPerfiles();
    GetUsuarios();

    $scope.perfil = {
        id: 0
    };


    $("#success").hide();
    $("#error").hide();

   function GetPerfiles() {



        $http({

            method: 'GET',
            url: '/Manager/GetPerfiles'

        }).then(function success(response) {
            
            $scope.Perfiles = response.data;

        }, function error(err) {
                alert(err.data);
        });

    }

    

  function GetUsuarios() {

      $scope.isLoading = true;

        $http({

            method: 'GET',
            url: '/Manager/GetUsuarios'

        }).then(function success(response) {

            $scope.Usuarios = response.data;
            $scope.isLoading = false;
        }, function error(err) {
                alert(err.data);
                $scope.isLoading = false;
        });

    }


    $scope.DesactivarUsuario = function (Id) {


        $http({

            method: 'POST',
            url: '/Manager/DesactivarUsuario?id=' + Id,

        }).then(function success(response) {

            swal.fire(response.data.message, response.data.messageInfo, response.data.messageType)
            GetUsuarios();
        }, function error(err) {
                alert(err.data);
        });
    }


    $scope.ModificarUsuario = function () {

        var datos = {

            Id: usuarioAEditar.Id,
            Usuario: $scope.nombreUsuario,
            Password: $scope.pass,
            Correo: $scope.correo,
            Descripcion: $scope.descripcion,
            PerfilId: $scope.perfil.id
        }

        $http({

            method: 'POST',
            url: '/Manager/ModificarUsuario',
            data: datos

        }).then(function success(response) {

            swal.fire(response.data.message, response.data.messageInfo, response.data.messageType)
            GetUsuarios();
            $scope.modify = false;
            $scope.nombreUsuario = "";
            $scope.pass = "";
            $scope.correo = "";
            $scope.descripcion = "";
            $scope.perfil = {
                id: 0

            };
        }, function error(err) {
                alert(err.data);
        });
    }

    $scope.LimpiarCrearUsuariosForm = function () {

        $scope.modify = false;
        $scope.nombreUsuario = "";
        $scope.pass = "";
        $scope.correo = "";
        $scope.descripcion = "";
        $scope.perfil = {
            id: 0

        };
    }


    $scope.EditUsuario = function (datos) {

        $scope.modify = true;
        usuarioAEditar = datos;

        $scope.nombreUsuario = usuarioAEditar.Usuario;
        $scope.pass = usuarioAEditar.Password;
        $scope.correo = usuarioAEditar.Correo;
        $scope.descripcion = usuarioAEditar.Descripcion;
        $scope.perfil = {
            id: usuarioAEditar.PerfilId

        };
    }

    $scope.ConfirmarPassword = function () {

        if ($scope.pass != '') {
            if ($scope.confirmPass == $scope.pass) {
                $("#confirmpass").css("border", "1px solid green");
                $("#success").show();
                $("#error").hide();
                $("#btncrear").prop("disabled", false);

            }
            else if ($scope.confirmPass != $scope.pass) {
                $("#confirmpass").css("border", "1px solid red");
                $("#error").show();
                $("#success").hide();
                $("#btncrear").prop("disabled", true);
            }
        }
    }    

    $scope.limpiarConfirmacionesPass = function () {

        if ($scope.pass == '') {
            $("#confirmpass").css("border", "1px solid #ced4da");
            $("#success").hide();
            $("#error").hide();
        }
    }  


    $scope.CreateUser = function () {

        var datos = {

            Usuario: $scope.nombreUsuario,
            Password: $scope.pass,
            Correo: $scope.correo,
            Descripcion: $scope.descripcion,
            PerfilId: $scope.perfil.id
        }

        $http({
            method: 'POST',
            url: '/Manager/CreateUser',
            data: datos
        }).then(function success(response) {
            swal.fire(response.data.message, response.data.messageInfo, response.data.messageType)


            $("#confirmpass").css("border", "1px solid #ced4da");
            $("#success").hide();
            $("#error").hide();

            $scope.nombreUsuario = '';
            $scope.pass = '';
            $scope.correo = '';
            $scope.confirmPass = '';
            $scope.perfil.id = 0;
            $scope.descripcion = '';

            GetUsuarios();

        }, function error(err) {

            swal.fire('Error!', err, 'error');

        })

    }

})