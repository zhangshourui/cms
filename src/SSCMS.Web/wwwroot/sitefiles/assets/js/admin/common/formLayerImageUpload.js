﻿var $url = '/common/form/layerImageUpload';

var data = utils.init({
  inputType: utils.getQueryString('inputType') || 'Image',
  attributeName: utils.getQueryString('attributeName'),
  no: utils.getQueryInt('no'),
  editorAttributeName: utils.getQueryString('editorAttributeName'),
  uploadUrl: null,
  dialogImageUrl: '',
  dialogVisible: false,
<<<<<<< HEAD
=======
  fileNames: [],
  files: [],
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  form: {
    userId: utils.getQueryInt('userId'),
    siteId: utils.getQueryInt('siteId'),
    isEditor: false,
    isMaterial: true,
    isThumb: false,
    thumbWidth: 500,
    thumbHeight: 500,
    isLinkToOriginal: true,
<<<<<<< HEAD
    filePaths: []
=======
    filePaths: [],
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  }
});

var methods = {
  parentInsert: function(no, result) {
    if (this.inputType === 'Image') {
      var vue = parent.$vue;
      if (vue.runFormLayerImageUploadText) {
        vue.runFormLayerImageUploadText(this.attributeName, no, result.imageVirtualUrl);
      }
      if (vue.runFormLayerImageUploadEditor && this.editorAttributeName && this.form.isEditor) {
        var html = '<img src="' + result.imageUrl + '" style="border: 0; max-width: 100%" />';
        if (result.previewUrl) {
          var previewUrl = "'" + result.previewUrl + "'";
          var vueHtml = '<el-image src="' + result.imageUrl + '" :preview-src-list="[' + previewUrl + ']" style="border: 0; max-width: 100%"></el-image>';
          html = '<img data-vue="' + encodeURIComponent(vueHtml) + '" src="' + result.imageUrl + '" style="border: 0; max-width: 100%" />';
        }
        vue.runFormLayerImageUploadEditor(this.editorAttributeName, html);
      }
    } else if (this.inputType === 'TextEditor') {
      if (parent.$vue.runEditorLayerImage) {
        var html = '<img src="' + result.imageUrl + '" style="border: 0; max-width: 100%" />';
        if (result.previewUrl) {
          var previewUrl = "'" + result.previewUrl + "'";
          var vueHtml = '<el-image src="' + result.imageUrl + '" :preview-src-list="[' + previewUrl + ']" style="border: 0; max-width: 100%"></el-image>';
          html = '<img data-vue="' + encodeURIComponent(vueHtml) + '" src="' + result.imageUrl + '" style="border: 0; max-width: 100%" />';
        }
        parent.$vue.runEditorLayerImage(this.attributeName, html);
      }
    }
  },

  apiGet: function() {
    var $this = this;

    utils.loading(this, true);
    $api.get($url, {
      params: {
        userId: this.form.userId,
        siteId: this.form.siteId
      }
    }).then(function(response) {
      var res = response.data;

      $this.form.isEditor = res.isEditor;
      $this.form.isMaterial = res.isMaterial;
      $this.form.isThumb = res.isThumb;
      $this.form.thumbWidth = res.thumbWidth;
      $this.form.thumbHeight = res.thumbHeight;
      $this.form.isLinkToOriginal = res.isLinkToOriginal;
    })
    .catch(function(error) {
      utils.error(error);
    })
    .then(function() {
      utils.loading($this, false);
    });
  },

  apiSubmit: function() {
    var $this = this;

    utils.loading(this, true);
<<<<<<< HEAD
=======
    this.form.filePaths = [];
    for (var i = 0; i < this.fileNames.length; i++) {
      var name = this.fileNames[i];
      for (var j = 0; j < this.files.length; j++) {
        var file = this.files[j];
        if (file.name === name) {
          this.form.filePaths.push(file.path);
          continue;
        }
      }
    }

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    $api.post($url, this.form).then(function(response) {
      var res = response.data;

      if (res && res.length > 0) {
        for (var i = 0; i < res.length; i++) {
          var result = res[i];
          $this.parentInsert($this.no + i, result);
        }
      }

      utils.closeLayer();
    })
    .catch(function(error) {
      utils.error(error);
    })
    .then(function() {
      utils.loading($this, false);
    });
  },

  btnSubmitClick: function () {
<<<<<<< HEAD
    if (this.form.filePaths.length === 0) {
=======
    if (this.files.length === 0) {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
      utils.error('请选择需要插入的图片文件！');
      return false;
    }

    this.apiSubmit();
  },

  btnCancelClick: function () {
    utils.closeLayer();
  },

  uploadBefore(file) {
<<<<<<< HEAD
    var isLt10M = file.size / 1024 / 1024 < 10;
    if (!isLt10M) {
      utils.error('上传图片大小不能超过 10MB!');
      return false;
    }
=======
    this.fileNames.push(file.name);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    return true;
  },

  uploadProgress: function() {
    utils.loading(this, true);
  },

  uploadSuccess: function(res) {
<<<<<<< HEAD
    this.form.filePaths.push(res.path);
=======
    this.files.push(res);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    utils.loading(this, false);
  },

  uploadError: function(err) {
    utils.loading(this, false);
    var error = JSON.parse(err.message);
    utils.error(error.message);
  },

  uploadRemove(file) {
    if (file.response) {
<<<<<<< HEAD
      this.form.filePaths.splice(this.form.filePaths.indexOf(file.response.path), 1);
=======
      var index = 0;
      for (var i = 0; i < this.files.length; i++) {
        var f = this.files[i];
        if (f.path === file.path) {
          index = i;
          break;
        }
      }
      this.files.splice(index, 1);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
  },

  uploadPictureCardPreview(file) {
    this.dialogImageUrl = file.url;
    this.dialogVisible = true;
  }
};

var $vue = new Vue({
  el: '#main',
  data: data,
  methods: methods,
  created: function () {
    utils.keyPress(this.btnSubmitClick, this.btnCancelClick);
    this.uploadUrl = $apiUrl + $url + '/actions/upload?siteId=' + this.form.siteId + '&userId=' + this.form.userId;
    this.apiGet();
  }
});
