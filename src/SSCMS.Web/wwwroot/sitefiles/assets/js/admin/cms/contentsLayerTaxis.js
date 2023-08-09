var $url = '/cms/contents/contentsLayerTaxis';

var data = utils.init({
  page: utils.getQueryInt('page'),
<<<<<<< HEAD
  form: {
    siteId: utils.getQueryInt('siteId'),
    channelId: utils.getQueryInt('channelId'),
    channelContentIds: utils.getQueryString('channelContentIds'),
    isUp: true,
    taxis: 1
=======
  siteId: utils.getQueryInt('siteId'),
  channelId: utils.getQueryInt('channelId'),
  channelContentIds: utils.getQueryString('channelContentIds'),
  contents: [],
  totalCount: 0,
  form: {
    type: 'to',
    value: 1
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  }
});

var methods = {
<<<<<<< HEAD
=======
  apiGet: function () {
    var $this = this;

    utils.loading(this, true);
    $api.get($url, {
      params: {
        siteId: this.siteId,
        channelId: this.channelId,
        channelContentIds: this.channelContentIds
      }
    }).then(function (response) {
      var res = response.data;

      $this.contents = res.contents;
      $this.totalCount = res.totalCount;
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  apiSubmit: function () {
    var $this = this;

    utils.loading(this, true);
<<<<<<< HEAD
    $api.post($url, this.form).then(function (response) {
      var res = response.data;

      parent.$vue.apiList($this.form.channelId, $this.page, '内容排序成功!');
=======
    $api.post($url, {
      siteId: this.siteId,
      channelId: this.channelId,
      channelContentIds: this.channelContentIds,
      type: this.form.type,
      value: this.form.value,
    }).then(function (response) {
      var res = response.data;

      parent.$vue.apiList($this.channelId, $this.page, '内容排序成功!');
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
      utils.closeLayer();
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

<<<<<<< HEAD
=======
  getTypeName: function () {
    if (this.form.type === 'to') {
      return '调整至';
    } else if (this.form.type === 'up') {
      return '上升数目';
    } else if (this.form.type === 'down') {
      return '下降数目';
    }
    return '';
  },

  getContentUrl: function (content) {
    if (content.checked) {
      return utils.getRootUrl('redirect', {
        siteId: content.siteId,
        channelId: content.channelId,
        contentId: content.id
      });
    }
    return $apiUrl + '/preview/' + content.siteId + '/' + content.channelId + '/' + content.id;
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
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
<<<<<<< HEAD
    utils.loading(this, false);
=======
    this.apiGet();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  }
});
