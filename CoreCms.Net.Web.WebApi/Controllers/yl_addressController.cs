/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:26
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

namespace CoreCms.Net.Web.WebApi.Controllers
{
    /// <summary>
    /// 
    ///</summary>
    [Description("")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [RequiredErrorForAdmin]
    [Authorize]
    public class yl_addressController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_addressServices _yl_addressServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_addressController(IWebHostEnvironment webHostEnvironment
            ,Iyl_addressServices yl_addressServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_addressServices = yl_addressServices;
        }

        #region 获取列表============================================================
        // POST: Api/yl_address/GetPageList
         /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取列表")]
        public async Task<AdminUiCallBack> GetPageList()
        {
            var jm = new AdminUiCallBack();
            var pageCurrent = Request.Form["page"].FirstOrDefault().ObjectToInt(1);
            var pageSize = Request.Form["limit"].FirstOrDefault().ObjectToInt(30);
            var where = PredicateBuilder.True<yl_address>();
            //获取排序字段
            var orderField = Request.Form["orderField"].FirstOrDefault();

            Expression<Func<yl_address, object>> orderEx = orderField switch
            {
                "id" => p => p.id,"userid" => p => p.userid,"address" => p => p.address,"name" => p => p.name,"phone" => p => p.phone,"createor" => p => p.createor,"createTime" => p => p.createTime,"modified" => p => p.modified,"modifyTime" => p => p.modifyTime,
                _ => p => p.id
            };

            //设置排序方式
            var orderDirection = Request.Form["orderDirection"].FirstOrDefault();
            var orderBy = orderDirection switch
            {
                "asc" => OrderByType.Asc,
                "desc" => OrderByType.Desc,
                _ => OrderByType.Desc
            };
            //查询筛选
			
			// int
			var id = Request.Form["id"].FirstOrDefault().ObjectToInt(0);
            if (id > 0)
            {
                where = where.And(p => p.id == id);
            }
			// int
			var userid = Request.Form["userid"].FirstOrDefault().ObjectToInt(0);
            if (userid > 0)
            {
                where = where.And(p => p.userid == userid);
            }
			// varchar
			var address = Request.Form["address"].FirstOrDefault();
            if (!string.IsNullOrEmpty(address))
            {
                where = where.And(p => p.address.Contains(address));
            }
			// varchar
			var name = Request.Form["name"].FirstOrDefault();
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(p => p.name.Contains(name));
            }
			// varchar
			var phone = Request.Form["phone"].FirstOrDefault();
            if (!string.IsNullOrEmpty(phone))
            {
                where = where.And(p => p.phone.Contains(phone));
            }
			// varchar
			var createor = Request.Form["createor"].FirstOrDefault();
            if (!string.IsNullOrEmpty(createor))
            {
                where = where.And(p => p.createor.Contains(createor));
            }
			// datetime
			var createTime = Request.Form["createTime"].FirstOrDefault();
            if (!string.IsNullOrEmpty(createTime))
            {
                if (createTime.Contains("到"))
                {
                    var dts = createTime.Split("到");
                    var dtStart = dts[0].Trim().ObjectToDate();
                    where = where.And(p => p.createTime > dtStart);
                    var dtEnd = dts[1].Trim().ObjectToDate();
                    where = where.And(p => p.createTime < dtEnd);
                }
                else
                {
                    var dt = createTime.ObjectToDate();
                    where = where.And(p => p.createTime > dt);
                }
            }
			// varchar
			var modified = Request.Form["modified"].FirstOrDefault();
            if (!string.IsNullOrEmpty(modified))
            {
                where = where.And(p => p.modified.Contains(modified));
            }
			// datetime
			var modifyTime = Request.Form["modifyTime"].FirstOrDefault();
            if (!string.IsNullOrEmpty(modifyTime))
            {
                if (modifyTime.Contains("到"))
                {
                    var dts = modifyTime.Split("到");
                    var dtStart = dts[0].Trim().ObjectToDate();
                    where = where.And(p => p.modifyTime > dtStart);
                    var dtEnd = dts[1].Trim().ObjectToDate();
                    where = where.And(p => p.modifyTime < dtEnd);
                }
                else
                {
                    var dt = modifyTime.ObjectToDate();
                    where = where.And(p => p.modifyTime > dt);
                }
            }
            //获取数据
            var list = await _yl_addressServices.QueryPageAsync(where, orderEx, orderBy, pageCurrent, pageSize, true);
            //返回数据
            jm.data = list;
            jm.code = 0;
            jm.count = list.TotalCount;
            jm.msg = "数据调用成功!";
            return jm;
        }
        #endregion

        #region 首页数据============================================================
        // POST: Api/yl_address/GetIndex
        /// <summary>
        /// 首页数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("首页数据")]
        public AdminUiCallBack GetIndex()
        {
            //返回数据
            var jm = new AdminUiCallBack { code = 0 };
            return jm;
        }
        #endregion

        #region 创建数据============================================================
        // POST: Api/yl_address/GetCreate
        /// <summary>
        /// 创建数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("创建数据")]
        public AdminUiCallBack GetCreate()
        {
            //返回数据
            var jm = new AdminUiCallBack { code = 0 };
            return jm;
        }
        #endregion

        #region 创建提交============================================================
        // POST: Api/yl_address/DoCreate
        /// <summary>
        /// 创建提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("创建提交")]
        public async Task<AdminUiCallBack> DoCreate([FromBody]yl_address entity)
        {
            var jm = new AdminUiCallBack();
            var result = await _yl_addressServices.InsertAsync(entity);
            if(result > 0)
            {
                jm.msg = "创建成功";
                jm.code = 0;
            }
            else
            {
                jm.msg = "创建失败";
                jm.code = 500;
            }
            return jm;
        }
        #endregion

        #region 编辑数据============================================================
        // POST: Api/yl_address/GetEdit
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("编辑数据")]
        public async Task<AdminUiCallBack> GetEdit([FromBody]FMIntId entity)
        {
            var jm = new AdminUiCallBack();

            var model = await _yl_addressServices.QueryByIdAsync(entity.id, false);
            if (model == null)
            {
                jm.msg = "不存在此信息";
                return jm;
            }
            jm.code = 0;
            jm.data = model;

            return jm;
        }
        #endregion

        #region 编辑提交============================================================
        // POST: Api/yl_address/Edit
        /// <summary>
        /// 编辑提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("编辑提交")]
        public async Task<AdminUiCallBack> DoEdit([FromBody]yl_address entity)
        {
            var jm = new AdminUiCallBack();
            var result = await _yl_addressServices.UpdateAsync(entity);
            if (result)
            {
                jm.msg = "编辑成功";
                jm.code = 0;
            }
            else
            {
                jm.msg = "编辑失败";
                jm.code = 500;
            }
            return jm;
        }
        #endregion

        #region 删除数据============================================================
        // POST: Api/yl_address/DoDelete/10
        /// <summary>
        /// 单选删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("单选删除")]
        public async Task<AdminUiCallBack> DoDelete([FromBody]FMIntId entity)
        {
            var jm = new AdminUiCallBack();

            var model = await _yl_addressServices.ExistsAsync(p => p.id == entity.id, true);
            if (!model)
            {
                jm.msg = GlobalConstVars.DataisNo;
				return jm;
            }
            var result = await _yl_addressServices.DeleteByIdAsync(entity.id);
            if (result)
            {
                jm.msg = "删除成功";
                jm.code = 0;
            }
            else
            {
                jm.msg = "删除失败";
                jm.code = 500;
            }
            return jm;
        }
        #endregion

        #region 批量删除============================================================
        // POST: Api/yl_address/DoBatchDelete/10,11,20
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("批量删除")]
        public async Task<AdminUiCallBack> DoBatchDelete([FromBody]FMArrayIntIds entity)
        {
            var jm = new AdminUiCallBack();
            var result = await _yl_addressServices.DeleteByIdsAsync(entity.id);
            if (result)
            {
                jm.msg = "批量删除成功";
                jm.code = 0;
            }
            else
            {
                jm.msg = "批量删除失败";
                jm.code = 500;
            }
            return jm;
        }

        #endregion

        #region 预览数据============================================================
        // POST: Api/yl_address/GetDetails/10
        /// <summary>
        /// 预览数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("预览数据")]
        public async Task<AdminUiCallBack> GetDetails([FromBody]FMIntId entity)
        {
            var jm = new AdminUiCallBack();

            var model = await _yl_addressServices.QueryByIdAsync(entity.id, false);
            if (model == null)
            {
                jm.msg = "不存在此信息";
                return jm;
            }
            jm.code = 0;
            jm.data = model;

            return jm;
        }
        #endregion

        #region 选择导出============================================================
        // POST: Api/yl_address/SelectExportExcel/10
        /// <summary>
        /// 选择导出
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("选择导出")]
        public async Task<AdminUiCallBack> SelectExportExcel([FromBody]FMArrayIntIds entity)
        {
            var jm = new AdminUiCallBack();

            //创建Excel文件的对象
            var book = new HSSFWorkbook();
            //添加一个sheet
            var mySheet = book.CreateSheet("Sheet1");
            //获取list数据
            var listModel = await _yl_addressServices.QueryListByClauseAsync(p => entity.id.Contains(p.id), p => p.id, OrderByType.Asc, true);
            //给sheet1添加第一行的头部标题
            var headerRow = mySheet.CreateRow(0);
            var headerStyle = ExcelHelper.GetHeaderStyle(book);

            var cell0 = headerRow.CreateCell(0);
            cell0.SetCellValue("");
            cell0.CellStyle = headerStyle;
            mySheet.SetColumnWidth(0, 10 * 256);

            var cell1 = headerRow.CreateCell(1);
            cell1.SetCellValue("");
            cell1.CellStyle = headerStyle;
            mySheet.SetColumnWidth(1, 10 * 256);

            var cell2 = headerRow.CreateCell(2);
            cell2.SetCellValue("");
            cell2.CellStyle = headerStyle;
            mySheet.SetColumnWidth(2, 10 * 256);

            var cell3 = headerRow.CreateCell(3);
            cell3.SetCellValue("");
            cell3.CellStyle = headerStyle;
            mySheet.SetColumnWidth(3, 10 * 256);

            var cell4 = headerRow.CreateCell(4);
            cell4.SetCellValue("");
            cell4.CellStyle = headerStyle;
            mySheet.SetColumnWidth(4, 10 * 256);

            var cell5 = headerRow.CreateCell(5);
            cell5.SetCellValue("");
            cell5.CellStyle = headerStyle;
            mySheet.SetColumnWidth(5, 10 * 256);

            var cell6 = headerRow.CreateCell(6);
            cell6.SetCellValue("");
            cell6.CellStyle = headerStyle;
            mySheet.SetColumnWidth(6, 10 * 256);

            var cell7 = headerRow.CreateCell(7);
            cell7.SetCellValue("");
            cell7.CellStyle = headerStyle;
            mySheet.SetColumnWidth(7, 10 * 256);

            var cell8 = headerRow.CreateCell(8);
            cell8.SetCellValue("");
            cell8.CellStyle = headerStyle;
            mySheet.SetColumnWidth(8, 10 * 256);

            headerRow.Height = 30 * 20;
            var commonCellStyle = ExcelHelper.GetCommonStyle(book);

            //将数据逐步写入sheet1各个行
            for (var i = 0; i < listModel.Count; i++)
            {
                var rowTemp = mySheet.CreateRow(i + 1);

                    var rowTemp0 = rowTemp.CreateCell(0);
                        rowTemp0.SetCellValue(listModel[i].id.ToString());
                        rowTemp0.CellStyle = commonCellStyle;

                    var rowTemp1 = rowTemp.CreateCell(1);
                        rowTemp1.SetCellValue(listModel[i].userid.ToString());
                        rowTemp1.CellStyle = commonCellStyle;

                    var rowTemp2 = rowTemp.CreateCell(2);
                        rowTemp2.SetCellValue(listModel[i].address.ToString());
                        rowTemp2.CellStyle = commonCellStyle;

                    var rowTemp3 = rowTemp.CreateCell(3);
                        rowTemp3.SetCellValue(listModel[i].name.ToString());
                        rowTemp3.CellStyle = commonCellStyle;

                    var rowTemp4 = rowTemp.CreateCell(4);
                        rowTemp4.SetCellValue(listModel[i].phone.ToString());
                        rowTemp4.CellStyle = commonCellStyle;

                    var rowTemp5 = rowTemp.CreateCell(5);
                        rowTemp5.SetCellValue(listModel[i].createor.ToString());
                        rowTemp5.CellStyle = commonCellStyle;

                    var rowTemp6 = rowTemp.CreateCell(6);
                        rowTemp6.SetCellValue(listModel[i].createTime.ToString());
                        rowTemp6.CellStyle = commonCellStyle;

                    var rowTemp7 = rowTemp.CreateCell(7);
                        rowTemp7.SetCellValue(listModel[i].modified.ToString());
                        rowTemp7.CellStyle = commonCellStyle;

                    var rowTemp8 = rowTemp.CreateCell(8);
                        rowTemp8.SetCellValue(listModel[i].modifyTime.ToString());
                        rowTemp8.CellStyle = commonCellStyle;

            }
            // 导出excel
            string webRootPath = _webHostEnvironment.WebRootPath;
            string tpath = "/files/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-yl_address导出(选择结果).xls";
            string filePath = webRootPath + tpath;
            DirectoryInfo di = new DirectoryInfo(filePath);
            if (!di.Exists)
            {
                di.Create();
            }
            FileStream fileHssf = new FileStream(filePath + fileName, FileMode.Create);
            book.Write(fileHssf);
            fileHssf.Close();

            jm.code = 0;
            jm.msg = GlobalConstVars.ExcelExportSuccess;
            jm.data = tpath + fileName;

            return jm;
        }
        #endregion

        #region 查询导出============================================================
        // POST: Api/yl_address/QueryExportExcel/10
        /// <summary>
        /// 查询导出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("查询导出")]
        public async Task<AdminUiCallBack> QueryExportExcel()
        {
            var jm = new AdminUiCallBack();

            var where = PredicateBuilder.True<yl_address>();
                //查询筛选
			
			// int
			var id = Request.Form["id"].FirstOrDefault().ObjectToInt(0);
            if (id > 0)
            {
                where = where.And(p => p.id == id);
            }
			// int
			var userid = Request.Form["userid"].FirstOrDefault().ObjectToInt(0);
            if (userid > 0)
            {
                where = where.And(p => p.userid == userid);
            }
			// varchar
			var address = Request.Form["address"].FirstOrDefault();
            if (!string.IsNullOrEmpty(address))
            {
                where = where.And(p => p.address.Contains(address));
            }
			// varchar
			var name = Request.Form["name"].FirstOrDefault();
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(p => p.name.Contains(name));
            }
			// varchar
			var phone = Request.Form["phone"].FirstOrDefault();
            if (!string.IsNullOrEmpty(phone))
            {
                where = where.And(p => p.phone.Contains(phone));
            }
			// varchar
			var createor = Request.Form["createor"].FirstOrDefault();
            if (!string.IsNullOrEmpty(createor))
            {
                where = where.And(p => p.createor.Contains(createor));
            }
			// datetime
			var createTime = Request.Form["createTime"].FirstOrDefault();
            if (!string.IsNullOrEmpty(createTime))
            {
                var dt = createTime.ObjectToDate();
                where = where.And(p => p.createTime > dt);
            }
			// varchar
			var modified = Request.Form["modified"].FirstOrDefault();
            if (!string.IsNullOrEmpty(modified))
            {
                where = where.And(p => p.modified.Contains(modified));
            }
			// datetime
			var modifyTime = Request.Form["modifyTime"].FirstOrDefault();
            if (!string.IsNullOrEmpty(modifyTime))
            {
                var dt = modifyTime.ObjectToDate();
                where = where.And(p => p.modifyTime > dt);
            }
            //获取数据
            //创建Excel文件的对象
            var book = new HSSFWorkbook();
            //添加一个sheet
            var mySheet = book.CreateSheet("Sheet1");
            //获取list数据
            var listModel = await _yl_addressServices.QueryListByClauseAsync(where, p => p.id, OrderByType.Asc, true);
            //给sheet1添加第一行的头部标题
                var headerRow = mySheet.CreateRow(0);
            var headerStyle = ExcelHelper.GetHeaderStyle(book);
            
            var cell0 = headerRow.CreateCell(0);
            cell0.SetCellValue("");
            cell0.CellStyle = headerStyle;
            mySheet.SetColumnWidth(0, 10 * 256);
			
            var cell1 = headerRow.CreateCell(1);
            cell1.SetCellValue("");
            cell1.CellStyle = headerStyle;
            mySheet.SetColumnWidth(1, 10 * 256);
			
            var cell2 = headerRow.CreateCell(2);
            cell2.SetCellValue("");
            cell2.CellStyle = headerStyle;
            mySheet.SetColumnWidth(2, 10 * 256);
			
            var cell3 = headerRow.CreateCell(3);
            cell3.SetCellValue("");
            cell3.CellStyle = headerStyle;
            mySheet.SetColumnWidth(3, 10 * 256);
			
            var cell4 = headerRow.CreateCell(4);
            cell4.SetCellValue("");
            cell4.CellStyle = headerStyle;
            mySheet.SetColumnWidth(4, 10 * 256);
			
            var cell5 = headerRow.CreateCell(5);
            cell5.SetCellValue("");
            cell5.CellStyle = headerStyle;
            mySheet.SetColumnWidth(5, 10 * 256);
			
            var cell6 = headerRow.CreateCell(6);
            cell6.SetCellValue("");
            cell6.CellStyle = headerStyle;
            mySheet.SetColumnWidth(6, 10 * 256);
			
            var cell7 = headerRow.CreateCell(7);
            cell7.SetCellValue("");
            cell7.CellStyle = headerStyle;
            mySheet.SetColumnWidth(7, 10 * 256);
			
            var cell8 = headerRow.CreateCell(8);
            cell8.SetCellValue("");
            cell8.CellStyle = headerStyle;
            mySheet.SetColumnWidth(8, 10 * 256);
			

            headerRow.Height = 30 * 20;
            var commonCellStyle = ExcelHelper.GetCommonStyle(book);

            //将数据逐步写入sheet1各个行
            for (var i = 0; i < listModel.Count; i++)
            {
                var rowTemp = mySheet.CreateRow(i + 1);


            var rowTemp0 = rowTemp.CreateCell(0);
            rowTemp0.SetCellValue(listModel[i].id.ToString());
            rowTemp0.CellStyle = commonCellStyle;



            var rowTemp1 = rowTemp.CreateCell(1);
            rowTemp1.SetCellValue(listModel[i].userid.ToString());
            rowTemp1.CellStyle = commonCellStyle;



            var rowTemp2 = rowTemp.CreateCell(2);
            rowTemp2.SetCellValue(listModel[i].address.ToString());
            rowTemp2.CellStyle = commonCellStyle;



            var rowTemp3 = rowTemp.CreateCell(3);
            rowTemp3.SetCellValue(listModel[i].name.ToString());
            rowTemp3.CellStyle = commonCellStyle;



            var rowTemp4 = rowTemp.CreateCell(4);
            rowTemp4.SetCellValue(listModel[i].phone.ToString());
            rowTemp4.CellStyle = commonCellStyle;



            var rowTemp5 = rowTemp.CreateCell(5);
            rowTemp5.SetCellValue(listModel[i].createor.ToString());
            rowTemp5.CellStyle = commonCellStyle;



            var rowTemp6 = rowTemp.CreateCell(6);
            rowTemp6.SetCellValue(listModel[i].createTime.ToString());
            rowTemp6.CellStyle = commonCellStyle;



            var rowTemp7 = rowTemp.CreateCell(7);
            rowTemp7.SetCellValue(listModel[i].modified.ToString());
            rowTemp7.CellStyle = commonCellStyle;



            var rowTemp8 = rowTemp.CreateCell(8);
            rowTemp8.SetCellValue(listModel[i].modifyTime.ToString());
            rowTemp8.CellStyle = commonCellStyle;


            }
            // 写入到excel
            string webRootPath = _webHostEnvironment.WebRootPath;
            string tpath = "/files/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-yl_address导出(查询结果).xls";
            string filePath = webRootPath + tpath;
            DirectoryInfo di = new DirectoryInfo(filePath);
            if (!di.Exists)
            {
                di.Create();
            }
            FileStream fileHssf = new FileStream(filePath + fileName, FileMode.Create);
            book.Write(fileHssf);
            fileHssf.Close();

            jm.code = 0;
            jm.msg = GlobalConstVars.ExcelExportSuccess;
            jm.data = tpath + fileName;

            return jm;
        }
        #endregion

        

    }
}
