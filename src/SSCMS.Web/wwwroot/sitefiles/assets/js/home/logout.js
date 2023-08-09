var $url = '/logout';

<<<<<<< HEAD
var data = utils.init({});
=======
var data = utils.init({
  returnUrl: utils.getQueryString('returnUrl')
});
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

var methods = {
  logout: function () {
    localStorage.removeItem(ACCESS_TOKEN_NAME);
    this.redirect();
  },

  redirect: function () {
<<<<<<< HEAD
    window.top.location.href = utils.getRootUrl('login');
=======
    if (this.returnUrl) {
      window.top.location.href = this.returnUrl;
    } else {
      window.top.location.href = utils.getRootUrl('login');
    }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  }
};

var $vue = new Vue({
  el: '#main',
  data: data,
  methods: methods,
  created: function () {
    this.logout();
  }
<<<<<<< HEAD
});
=======
});
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
