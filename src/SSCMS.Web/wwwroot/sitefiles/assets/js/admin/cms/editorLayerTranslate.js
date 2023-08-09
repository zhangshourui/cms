var $url = '/cms/contents/editorLayerTranslate';

var data = utils.init({
  siteId: utils.getQueryInt('siteId'),
  channelId: utils.getQueryInt('channelId'),
  transSites: null,
  transChannels: null,
  form: {
    transSiteIds: null,
    transChannelIds: null,
    transType: 'Copy',
  }
});

var methods = {
  apiGet: function () {
    var $this = this;

    utils.loading(this, true);
    $api.get($url, {
      params: {
        siteId: this.siteId,
        channelId: this.channelId,
      }
    }).then(function (response) {
      var res = response.data;

      $this.transSites = res.transSites;
      $this.form.transSiteIds = [$this.siteId];
      $this.apiGetOptions();
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiGetOptions: function() {
    var $this = this;

    $api.post($url + '/actions/options', {
      siteId: this.siteId,
      channelId: this.channelId,
      transSiteId: this.form.transSiteIds[this.form.transSiteIds.length - 1],
    }).then(function (response) {
      var res = response.data;

      $this.transChannels = [res.transChannels];
      $this.form.transChannelIds = null;
    }).catch(function (error) {
      utils.loading($this, false);
      utils.error(error);
    });
  },

  apiSubmit: function() {
    var $this = this;

    var transChannelIds = [];
    this.form.transChannelIds.forEach(function(arr) {
      var transChannelId = arr[arr.length - 1];
      transChannelIds.push(transChannelId);
    });

    var transSiteId = this.form.transSiteIds[this.form.transSiteIds.length - 1];
<<<<<<< HEAD
=======
    var transType = this.siteId !== transSiteId ? 'Copy' : this.form.transType;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

    utils.loading(this, true);
    $api.post($url, {
      siteId: this.siteId,
      channelId: this.channelId,
      transSiteId: transSiteId,
      transChannelIds: transChannelIds,
<<<<<<< HEAD
      transType: this.form.transType
=======
      transType: transType
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }).then(function (response) {
      var res = response.data;

      var channels = res.channels;
      channels.forEach(function (channel) {
        parent.$vue.addTranslation(
          transSiteId,
          channel.id,
<<<<<<< HEAD
          $this.form.transType,
=======
          transType,
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
          channel.name
        );
      });

      utils.closeLayer();
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

<<<<<<< HEAD
=======
  isCopyType: function () {
    var transSiteId = this.form.transSiteIds[this.form.transSiteIds.length - 1];
    return this.siteId === transSiteId;
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  handleTransSiteIdChange: function() {
    this.apiGetOptions();
  },

  btnSubmitClick: function () {
    var $this = this;

    this.$refs.form.validate(function(valid) {
      if (valid) {
        $this.apiSubmit();
      }
    });
  },

  btnCancelClick: function () {
    utils.closeLayer();
  },
};

var $vue = new Vue({
  el: '#main',
  data: data,
  methods: methods,
  created: function () {
    utils.keyPress(this.btnSubmitClick, this.btnCancelClick);
    this.apiGet();
  }
});
