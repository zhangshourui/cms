﻿var $url = '/cms/channels/channelsLayerCreate';

var data = utils.init({
  siteId: utils.getQueryInt('siteId'),
  channelIds: utils.getQueryIntList('channelIds'),
  form: {
    isIncludeChildren: false,
    isCreateContents: false
  }
});

var methods = {
  apiSubmit: function() {
    var $this = this;

    utils.loading(this, true);
    $api.post($url, {
      siteId: this.siteId,
      channelIds: this.channelIds,
      isIncludeChildren: this.form.isIncludeChildren,
      isCreateContents: this.form.isCreateContents
    }).then(function (response) {
      var res = response.data;

<<<<<<< HEAD
      parent.$vue.apiList(res);
      utils.success('页面生成成功!');
=======
      utils.addTab('生成进度查看', utils.getCmsUrl('createStatus', {siteId: $this.siteId}));
      parent.$vue.apiList(res);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
      utils.closeLayer();
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
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
    utils.loading(this, false);
  }
});
