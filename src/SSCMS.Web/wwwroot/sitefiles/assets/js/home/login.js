var $url = '/login';
var $urlCaptcha = '/login/captcha';
var $urlCaptchaCheck = '/login/captcha/actions/check';
var $urlSendSms = '/login/actions/sendSms';

var data = utils.init({
  status: utils.getQueryInt('status'),
  pageAlert: null,
  captchaToken: null,
  captchaUrl: null,
  version: null,
  homeTitle: null,
  isSmsEnabled: false,
<<<<<<< HEAD
=======
  isUserCaptchaDisabled: false,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  countdown: 0,
  form: {
    type: 'account',
    account: null,
    password: null,
    mobile: null,
    code: null,
    isPersistent: false,
    captchaValue: null,
  },
  returnUrl: utils.getQueryString('returnUrl')
});

var methods = {
  apiGet: function () {
    var $this = this;

    if (this.status === 401) {
      this.pageAlert = {
        type: 'error',
        title: '您的账号登录已过期或失效，请重新登录'
      };
    }
<<<<<<< HEAD
    var tempUrl = $url;
    var access_token = utils.getQueryString("access_token");
    if (access_token) {
      tempUrl += "?access_token=" + encodeURIComponent(access_token);
    }

    utils.loading(this, true);
    $api.get(tempUrl).then(function (response) {
      var res = response.data;
      if (res.token) {
        localStorage.removeItem(ACCESS_TOKEN_NAME);
        localStorage.setItem(ACCESS_TOKEN_NAME, res.token);
        if ($this.returnUrl) {
          location.href = $this.returnUrl;
        } else {
          $this.redirectIndex();
        }
      }
      else {

        $this.version = res.version;
        $this.homeTitle = res.homeTitle;
        $this.isSmsEnabled = res.isSmsEnabled;
=======

    utils.loading(this, true);
    $api.get($url).then(function (response) {
      var res = response.data;

      $this.version = res.version;
      $this.homeTitle = res.homeTitle;
      $this.isSmsEnabled = res.isSmsEnabled;
      $this.isUserCaptchaDisabled = res.isUserCaptchaDisabled;
      if ($this.isUserCaptchaDisabled) {
        $this.btnTypeClick();
      } else {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        $this.apiCaptcha();
      }
    }).catch(function (error) {
      utils.notifyError(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiCaptcha: function () {
    var $this = this;

    utils.loading(this, true);
    $api.post($urlCaptcha).then(function (response) {
      var res = response.data;

      $this.captchaToken = res.value;
      $this.captchaUrl = $apiUrl + $urlCaptcha + '?token=' + $this.captchaToken;
      $this.btnTypeClick();
    }).catch(function (error) {
      utils.notifyError(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiCaptchaCheck: function () {
    var $this = this;
<<<<<<< HEAD

    utils.loading(this, true);
    $api.post($urlCaptchaCheck, {
      token: this.captchaToken,
      value: this.form.captchaValue
    }).then(function (response) {
      $this.apiSubmit();
    }).catch(function (error) {
      $this.apiCaptcha();
      utils.loading($this, false);
      utils.notifyError(error);
    });
=======
    if (this.isUserCaptchaDisabled) {
      $this.apiSubmit();
    } else {
      utils.loading(this, true);
      $api.post($urlCaptchaCheck, {
        token: this.captchaToken,
        value: this.form.captchaValue
      }).then(function (response) {
        $this.apiSubmit();
      }).catch(function (error) {
        $this.apiCaptcha();
        utils.loading($this, false);
        utils.notifyError(error);
      });
    }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  },

  apiSendSms: function () {
    var $this = this;

    utils.loading(this, true);
    $api.post($urlSendSms, {
      mobile: this.form.mobile
    }).then(function (response) {
      var res = response.data;

      utils.notifySuccess('验证码发送成功，10分钟内有效');
      $this.countdown = 60;
      var interval = setInterval(function () {
        $this.countdown -= 1;
<<<<<<< HEAD
        if ($this.countdown <= 0) {
=======
        if ($this.countdown <= 0){
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
          clearInterval(interval);
        }
      }, 1000);
    }).catch(function (error) {
      utils.notifyError(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiSubmit: function () {
    var $this = this;

    utils.loading(this, true);
    $api.post($url, {
      isSmsLogin: this.form.type == 'mobile',
      account: this.form.account,
      password: this.form.password ? md5(this.form.password) : '',
      mobile: this.form.mobile,
      code: this.form.code,
      isPersistent: this.form.isPersistent
    }).then(function (response) {
      var res = response.data;

      localStorage.removeItem(ACCESS_TOKEN_NAME);
      localStorage.setItem(ACCESS_TOKEN_NAME, res.token);
      if (res.redirectToVerifyMobile) {
        location.href = utils.getRootUrl('verifyMobile', {
          returnUrl: $this.returnUrl
        });
      } else if ($this.returnUrl) {
        location.href = $this.returnUrl;
      } else {
        $this.redirectIndex();
      }
    }).catch(function (error) {
      utils.notifyError(error);
    }).then(function () {
      $this.apiCaptcha();
      utils.loading($this, false);
    });
  },

<<<<<<< HEAD
  //redirectIndex: function () {
  //  location.href = utils.getIndexUrl();
  //},
  redirectIndex: function () {
    location.href = "/"
  },
=======
  redirectIndex: function () {
    location.href = utils.getIndexUrl();
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  redirectLostPassword: function () {
    location.href = utils.getRootUrl('lostPassword');
  },

<<<<<<< HEAD
  btnTypeClick: function () {
    var $this = this;

    this.$refs.formAccount.clearValidate();
    this.$refs.formMobile.clearValidate();
=======
  btnTypeClick: function() {
    var $this = this;

    this.$refs.formAccount && this.$refs.formAccount.clearValidate();
    this.$refs.formMobile && this.$refs.formMobile.clearValidate();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    if (this.form.type == 'account') {
      setTimeout(function () {
        $this.$refs['account'].focus();
      }, 100);
    } else if (this.form.type == 'mobile') {
      setTimeout(function () {
        $this.$refs['mobile'].focus();
      }, 100);
    }
  },

  btnCaptchaClick: function () {
    this.apiCaptcha();
  },

  isMobile: function (value) {
    return /^1[3-9]\d{9}$/.test(value);
  },

  btnSendSmsClick: function () {
    if (this.countdown > 0) return;
    if (!this.form.mobile) {
      utils.notifyError('手机号码不能为空');
      return;
    } else if (!this.isMobile(this.form.mobile)) {
      utils.notifyError('请输入有效的手机号码');
      return;
    }

    this.apiSendSms();
  },

  btnSubmitClick: function () {
    var $this = this;

    if (this.form.type == 'account') {
<<<<<<< HEAD
      this.$refs.formAccount.validate(function (valid) {
=======
      this.$refs.formAccount.validate(function(valid) {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        if (valid) {
          $this.apiCaptchaCheck();
        }
      });
    } else {
<<<<<<< HEAD
      this.$refs.formMobile.validate(function (valid) {
=======
      this.$refs.formMobile.validate(function(valid) {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        if (valid) {
          $this.apiSubmit();
        }
      });
    }
  },

<<<<<<< HEAD
  btnRegisterClick: function (e) {
=======
  btnRegisterClick: function(e) {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    e.preventDefault();
    location.href = utils.getRootUrl('register');
  }
};

var $vue = new Vue({
  el: '#main',
  data: data,
  methods: methods,
  created: function () {
    this.apiGet();
  }
});
