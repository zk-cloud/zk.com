/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/6/6 17:22:48
 *        Description: 暂无
 ***********************************************************************/


using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.Entities.Expression;
using CoreCms.Net.Model.FromBody;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Filter;
using CoreCms.Net.Loging;
using CoreCms.Net.IServices;
using CoreCms.Net.Utility.Helper;
using CoreCms.Net.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using SqlSugar;
using CoreCms.Net.Services;

namespace CoreCms.Net.Web.Admin.Controllers
{
    /// <summary>
    /// 
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class yl_filesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_filesServices _yl_filesServices;
        private readonly ICoreCmsSettingServices _coreCmsSettingServices;
        private readonly IToolsServices _toolsServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_filesController(IWebHostEnvironment webHostEnvironment
            ,Iyl_filesServices yl_filesServices
            , ICoreCmsSettingServices coreCmsSettingServices
            , IToolsServices toolsServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_filesServices = yl_filesServices;
            _coreCmsSettingServices = coreCmsSettingServices;
            _toolsServices = toolsServices;
        }

        #region 通用上传接口====================================================

        /// <summary>
        ///     通用上传接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> UploadFiles()
        {
            var jm = new WebApiCallBack();

            var filesStorageOptions = await _coreCmsSettingServices.GetFilesStorageOptions();

            //初始化上传参数
            var maxSize = 1024 * 1024 * filesStorageOptions.MaxSize; //上传大小5M

            var file = Request.Form.Files["file"];
            if (file == null)
            {
                jm.msg = "请选择文件";
                return jm;
            }

            var fileName = file.FileName;
            var fileExt = Path.GetExtension(fileName).ToLowerInvariant();

            //检查大小
            if (file.Length > maxSize)
            {
                jm.msg = "上传文件大小超过限制，最大允许上传" + filesStorageOptions.MaxSize + "M";
                return jm;
            }

            //检查文件扩展名
            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(filesStorageOptions.FileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                jm.msg = "上传文件扩展名是不允许的扩展名,请上传后缀名为：" + filesStorageOptions.FileTypes;
                return jm;
            }

            string url = string.Empty;
            if (filesStorageOptions.StorageType == GlobalEnumVars.FilesStorageOptionsType.LocalStorage.ToString())
            {
                url = await _toolsServices.UpLoadFileForLocalStorage(filesStorageOptions, fileExt, file);
            }
            else if (filesStorageOptions.StorageType == GlobalEnumVars.FilesStorageOptionsType.AliYunOSS.ToString())
            {
                url = await _toolsServices.UpLoadFileForAliYunOSS(filesStorageOptions, fileExt, file);
            }
            else if (filesStorageOptions.StorageType == GlobalEnumVars.FilesStorageOptionsType.QCloudOSS.ToString())
            {
                url = await _toolsServices.UpLoadFileForQCloudOSS(filesStorageOptions, fileExt, file);
            }
            else if (filesStorageOptions.StorageType == GlobalEnumVars.FilesStorageOptionsType.QiNiuKoDo.ToString())
            {
                url = await _toolsServices.UpLoadFileForQiNiuKoDo(filesStorageOptions, fileExt, file);
            }

            var bl = !string.IsNullOrEmpty(url);
            jm.code = bl ? 0 : 1;
            jm.msg = bl ? "上传成功!" : "上传失败";
            jm.data = new
            {
                fileUrl = url,
                src = url
            };

            return jm;
        }

        #endregion
    }
}
