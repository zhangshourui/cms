var $url = "/error";

var data = utils.init({
  logId: utils.getQueryInt('logId'),
  uuid: utils.getQueryString('uuid'),
  message: utils.getQueryString('message'),
  stackTrace: null,
<<<<<<< HEAD
  addDate: null
=======
  createdDate: null
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
});

var methods = {
  apiGet: function() {
    var $this = this;

    utils.loading(this, true);
    $api.get($url, {
      params: {
        logId: this.logId
      }
    }).then(function (response) {
      var res = response.data;

<<<<<<< HEAD
      $this.message = res.error.summary + ' ' + res.error.message;
      $this.stackTrace = res.error.stackTrace;
      $this.addDate = res.error.addDate;
=======
      $this.message = res.summary + ' ' + res.message;
      $this.stackTrace = res.stackTrace;
      $this.createdDate = res.createdDate;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  }
};

var $vue = new Vue({
  el: "#main",
  data: data,
  methods: methods,
  created: function () {
    if (this.logId > 0) {
      this.apiGet();
    } else if (this.uuid) {
<<<<<<< HEAD
      this.message = sessionStorage.getItem(this.uuid);
      utils.loading(this, false);
    }
  },
});
=======
      var error = JSON.parse(sessionStorage.getItem(this.uuid));
      this.message = error.message;
      this.stackTrace = error.stackTrace;
      this.createdDate = error.createdDate;
      utils.loading(this, false);
    }
  },
});
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
