var $url = '/cms/templates/templatesAssets';
var $urlDelete = $url + '/actions/delete';
<<<<<<< HEAD
=======
var $urlConfig = $url + '/actions/config';
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

var data = utils.init({
  siteId: utils.getQueryInt("siteId"),
  directories: null,
  allFiles: null,
  files: null,
  siteUrl: null,
<<<<<<< HEAD
  includeDir: null,
  cssDir: null,
  jsDir: null,
  fileType: utils.getQueryString("fileType") || 'All',
=======
  cssDir: null,
  jsDir: null,
  imagesDir: null,
  fileType: utils.getQueryString("fileType") || 'css',
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  directoryPaths: [],
  keyword: '',

  configPanel: false,
  configParent: null,
  configForm: null,
});

var methods = {
<<<<<<< HEAD
  apiList: function () {
=======
  apiGet: function () {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    var $this = this;

    utils.loading(this, true);
    $api.get($url, {
      params: {
        siteId: this.siteId,
        fileType: this.fileType
      }
    }).then(function (response) {
      var res = response.data;

<<<<<<< HEAD
      $this.directories = res.directories;
      $this.allFiles = res.files;
      $this.siteUrl = res.siteUrl;
      $this.includeDir = res.includeDir;
      $this.cssDir = res.cssDir;
      $this.jsDir = res.jsDir;
=======
      $this.directoryPaths = [];
      $this.keyword = '';

      $this.directories = res.directories;
      $this.allFiles = res.files;
      $this.siteUrl = res.siteUrl;
      $this.cssDir = res.cssDir;
      $this.jsDir = res.jsDir;
      $this.imagesDir = res.imagesDir;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
      $this.reload();
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiDelete: function (file) {
    var $this = this;

    utils.loading(this, true);
    $api.post($urlDelete, {
      siteId: this.siteId,
      fileType: this.fileType,
      directoryPath: file.directoryPath,
      fileName: file.fileName
    }).then(function (response) {
      var res = response.data;

      $this.allFiles.splice($this.allFiles.indexOf(file), 1);
      $this.reload();
      utils.success('文件删除成功！');
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiConfig: function () {
    var $this = this;

    this.loading = this.$loading();
<<<<<<< HEAD
    $api.post($url + '/actions/config', this.configForm).then(function (response) {
      var res = response.data;

      $this.directories = res.directories;
      $this.allFiles = res.files;
      $this.siteUrl = res.siteUrl;
      $this.includeDir = res.includeDir;
      $this.cssDir = res.cssDir;
      $this.jsDir = res.jsDir;
      $this.reload();

      $this.configPanel = false;
      utils.success('文件夹路径设置成功!');
=======
    $api.post($urlConfig, this.configForm).then(function (response) {
      var res = response.data;

      $this.configPanel = false;
      utils.success('文件夹路径设置成功!');

      $this.apiGet();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  getFileType: function(fileType) {
<<<<<<< HEAD
    if (fileType === 'html') {
      return '包含文件';
    } else if (fileType === 'css') {
      return '样式文件';
    } else if (fileType === 'js') {
      return '脚本文件';
=======
    if (fileType === 'css') {
      return '样式文件';
    } else if (fileType === 'js') {
      return '脚本文件';
    } else if (fileType === 'images') {
      return '图片文件';
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
    return '';
  },

<<<<<<< HEAD
  btnDefaultClick: function (template) {
    var $this = this;

    utils.alertWarning({
      title: '设置默认文件',
      text: '此操作将把文件 ' + template.templateName + ' 设为默认' + this.getTemplateType(template.fileType) + '，确认吗？',
      callback: function () {
        $this.apiDefault(template);
      }
    });
  },

  btnCopyClick: function(template) {
    var $this = this;

    utils.loading(this, true);
    $api.post($url + '/actions/copy', {
      siteId: this.siteId,
      templateId: template.id
    }).then(function (response) {
      var res = response.data;

      $this.directories = res.directories;
      $this.allFiles = res.files;
      $this.reload();
      utils.success('快速复制成功！');
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
=======
  btnNavClick: function() {
    this.apiGet();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  },

  btnDeleteClick: function (file) {
    var $this = this;

    utils.alertDelete({
      title: '删除文件',
      text: '此操作将删除文件 ' + file.directoryPath + '/' + file.fileName + '，确认吗？',
      callback: function () {
        $this.apiDelete(file);
      }
    });
  },

  btnAddClick: function(fileType) {
    utils.addTab('新增' + this.getFileType(fileType), this.getEditorUrl('', '', fileType));
  },

  btnEditClick: function(file) {
<<<<<<< HEAD
=======
    if (this.fileType == 'images') {
      var url = this.getPageUrl(file.directoryPath + '/' + file.fileName);
      window.open(url);
      return;
    }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    utils.addTab('编辑' + ':' + file.directoryPath + '/' + file.fileName, this.getEditorUrl(file.directoryPath, file.fileName, file.fileType));
  },

  getEditorUrl: function(directoryPath, fileName, fileType) {
    return utils.getCmsUrl('templatesAssetsEditor', {
      siteId: this.siteId,
      directoryPath: directoryPath,
      fileName: fileName,
      fileType: fileType,
      tabName: utils.getTabName()
    });
  },

<<<<<<< HEAD
  getPageUrl: function(directoryPath) {
    return this.siteUrl + '/' + directoryPath;
  },

=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  reload: function() {
    var $this = this;

    this.files = _.filter(this.allFiles, function(o) {
<<<<<<< HEAD
      var isFileType = true;
      var isDirectoryPath = true;
      var isKeyword = true;
      if ($this.fileType != 'All') {
        isFileType = _.endsWith(o.fileName, $this.fileType);
      }
=======
      var isDirectoryPath = true;
      var isKeyword = true;
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
      if ($this.directoryPaths.length > 0) {
        isDirectoryPath = false;
        for (var i = 0; i < $this.directoryPaths.length; i++) {
          var directoryPath = $this.directoryPaths[i][$this.directoryPaths[i].length - 1];
          if (o.directoryPath == directoryPath) {
            isDirectoryPath = true;
          }
        }
      }
      if ($this.keyword) {
        isKeyword = (o.directoryPath || '').indexOf($this.keyword) !== -1 || (o.fileName || '').indexOf($this.keyword) !== -1;
      }

<<<<<<< HEAD
      return isFileType && isDirectoryPath && isKeyword;
    });
  },

=======
      return isDirectoryPath && isKeyword;
    });
  },

  getPageUrl: function(directoryPath) {
    return this.siteUrl + '/' + directoryPath;
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  btnConfigClick: function() {
    this.configForm = {
      siteId: this.siteId,
      fileType: this.fileType,
<<<<<<< HEAD
      includeDir: this.includeDir,
      cssDir: this.cssDir,
      jsDir: this.jsDir
=======
      cssDir: this.cssDir,
      jsDir: this.jsDir,
      imagesDir: this.imagesDir
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    };
    this.configPanel = true;
  },

  btnConfigCancelClick: function() {
    this.configPanel = false;
  },

  btnConfigSubmitClick: function() {
    var $this = this;
    this.$refs.configForm.validate(function(valid) {
      if (valid) {
        $this.apiConfig();
      }
    });
  },

<<<<<<< HEAD
=======
  getUploadUrl: function() {
    var directories = '';
    if (this.directoryPaths.length > 0) {
      var arr = this.directoryPaths[this.directoryPaths.length - 1];
      directories = arr[arr.length - 1];
    }
    return $apiUrl + $url + '/actions/upload?siteId=' + this.siteId + '&fileType=' + this.fileType + '&directories=' + encodeURIComponent(directories);
  },

  uploadBefore(file) {
    return true;
  },

  uploadProgress: function() {
    utils.loading(this, true);
  },

  uploadSuccess: function(res) {
    utils.success('文件上传成功!');
    this.apiGet();
  },

  uploadError: function(err) {
    utils.loading(this, false);
    var error = JSON.parse(err.message);
    utils.error(error.message);
  },

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  btnCloseClick: function() {
    utils.removeTab();
  },
};

var $vue = new Vue({
  el: '#main',
  data: data,
  methods: methods,
  created: function () {
    var $this = this;
    utils.keyPress(function() {
      if ($this.configPanel) {
        $this.btnConfigSubmitClick();
      }
    }, function() {
      if ($this.configPanel) {
        $this.btnConfigCancelClick();
      } else {
        $this.btnCloseClick();
      }
    });
<<<<<<< HEAD
    this.apiList();
=======
    this.apiGet();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
  }
});
