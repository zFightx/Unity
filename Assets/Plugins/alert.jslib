mergeInto(LibraryManager.library, {

  JSAlert: function (mensagem) {
    window.alert(UTF8ToString(mensagem));
  },

  GetHello: function () {
    return 10;
  },
});