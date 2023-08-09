﻿var $url = '/cms/contents/contentsLayerImport';

var data = utils.init({
  checkedLevels: null,
  form: {
    siteId: utils.getQueryInt('siteId'),
    channelId: utils.getQueryInt('channelId'),
    importType: 'zip',
    checkedLevel: null,
    isOverride: false,
<<<<<<< HEAD
    fileNames: []
=======
    fileNames: [],
    fileUrls: [],
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  },
  uploadUrl: null,
  uploadList: []
});

var methods = {
  apiGet: function () {
    var $this = this;

    utils.loading(this, true);
    $api.get($url, {
      params: {
        siteId: this.form.siteId,
        channelId: this.form.channelId
      }
    }).then(function (response) {
      var res = response.data;

      $this.checkedLevels = res.checkedLevels;
      $this.form.checkedLevel = res.value;
<<<<<<< HEAD
      $this.uploadUrl = $apiUrl + $url + '/actions/upload?siteId=' + $this.form.siteId + '&channelId=' + $this.form.channelId;
=======
      $this.form.importType = res.options.importType;
      $this.form.isOverride = res.options.isOverride;

      $this.btnRadioInput();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiSubmit: function (){
    var $this = this;

    utils.loading(this, true);
    $api.post($url, this.form).then(function (response) {
      var res = response.data;

      utils.closeLayer();
      parent.$vue.apiList($this.form.channelId, 1, '文件导入成功！', true);
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

<<<<<<< HEAD
=======
  btnRadioInput: function () {
    this.uploadUrl = $apiUrl + $url + '/actions/upload?siteId=' + this.form.siteId + '&channelId=' + this.form.channelId + '&importType=' + this.form.importType;
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  btnSubmitClick: function () {
    if (this.form.fileNames.length === 0) {
      return utils.error('请选择需要导入的文件！');
    }

    this.apiSubmit();
  },

  btnCancelClick: function () {
    utils.closeLayer();
  },

  uploadBefore(file) {
    var re = /(\.zip|\.xlsx|\.txt)$/i;
<<<<<<< HEAD
    if (this.importType === 'zip') {
      re = /(\.zip)$/i;
    } else if (this.importType === 'xlsx') {
      re = /(\.xlsx)$/i;
    } else if (this.importType === 'txt') {
      re = /(\.txt)$/i;
    }
    if(!re.exec(file.name))
    {
=======
    if (this.form.importType === 'zip') {
      re = /(\.zip)$/i;
    } else if (this.form.importType === 'xlsx') {
      re = /(\.xlsx)$/i;
    } else if (this.form.importType === 'image') {
      re = /(\.jpg|\.jpeg|\.bmp|\.gif|\.png|\.webp)$/i;
    } else if (this.form.importType === 'txt') {
      re = /(\.txt)$/i;
    }
    if(!re.exec(file.name)) {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
      utils.error('请选择有效的文件上传!');
      return false;
    }
    return true;
  },

  uploadProgress: function() {
    utils.loading(this, true);
  },

  uploadRemove(file) {
    if (file.response) {
<<<<<<< HEAD
      this.form.fileNames.splice(this.form.fileNames.indexOf(file.response.name), 1);
=======
      var index = this.form.fileNames.indexOf(file.response.name);
      this.form.fileNames.splice(index, 1);
      this.form.fileUrls.splice(index, 1);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
  },

  uploadSuccess: function(res) {
    this.form.fileNames.push(res.name);
<<<<<<< HEAD
=======
    this.form.fileUrls.push(res.url);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    utils.loading(this, false);
  },

  uploadError: function(err) {
    utils.loading(this, false);
    var error = JSON.parse(err.message);
    utils.error(error.message);
  }
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
