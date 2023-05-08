/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:46
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
    public class yl_ordersController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_ordersServices _yl_ordersServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_ordersController(IWebHostEnvironment webHostEnvironment
            ,Iyl_ordersServices yl_ordersServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_ordersServices = yl_ordersServices;
        }

        #region 获取列表============================================================
        // POST: Api/yl_orders/GetPageList
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
            var where = PredicateBuilder.True<yl_orders>();
            //获取排序字段
            var orderField = Request.Form["orderField"].FirstOrDefault();

            Expression<Func<yl_orders, object>> orderEx = orderField switch
            {
                "id" => p => p.id,"number" => p => p.number,"orderType" => p => p.orderType,"userid" => p => p.userid,"driverid" => p => p.driverid,"sneder" => p => p.sneder,"sendAddress" => p => p.sendAddress,"snedPhone" => p => p.snedPhone,"recipient" => p => p.recipient,"receAddress" => p => p.receAddress,"recePhone" => p => p.recePhone,"carType" => p => p.carType,"pay" => p => p.pay,"amount" => p => p.amount,"settlementNum" => p => p.settlementNum,"remark" => p => p.remark,"itemName" => p => p.itemName,"itemType" => p => p.itemType,"itemWeight" => p => p.itemWeight,"itemVolume" => p => p.itemVolume,"itemNum" => p => p.itemNum,"itemPictures" => p => p.itemPictures,"deliveryTime" => p => p.deliveryTime,"isDelete" => p => p.isDelete,"isComfirm" => p => p.isComfirm,"state" => p => p.state,"createor" => p => p.createor,"createTime" => p => p.createTime,"modified" => p => p.modified,"modifyTime" => p => p.modifyTime,
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
			// varchar
			var number = Request.Form["number"].FirstOrDefault();
            if (!string.IsNullOrEmpty(number))
            {
                where = where.And(p => p.number.Contains(number));
            }
			// varchar
			var orderType = Request.Form["orderType"].FirstOrDefault();
            if (!string.IsNullOrEmpty(orderType))
            {
                where = where.And(p => p.orderType.Contains(orderType));
            }
			// int
			var userid = Request.Form["userid"].FirstOrDefault().ObjectToInt(0);
            if (userid > 0)
            {
                where = where.And(p => p.userid == userid);
            }
			// int
			var driverid = Request.Form["driverid"].FirstOrDefault().ObjectToInt(0);
            if (driverid > 0)
            {
                where = where.And(p => p.driverid == driverid);
            }
			// varchar
			var sneder = Request.Form["sneder"].FirstOrDefault();
            if (!string.IsNullOrEmpty(sneder))
            {
                where = where.And(p => p.sneder.Contains(sneder));
            }
			// varchar
			var sendAddress = Request.Form["sendAddress"].FirstOrDefault();
            if (!string.IsNullOrEmpty(sendAddress))
            {
                where = where.And(p => p.sendAddress.Contains(sendAddress));
            }
			// varchar
			var snedPhone = Request.Form["snedPhone"].FirstOrDefault();
            if (!string.IsNullOrEmpty(snedPhone))
            {
                where = where.And(p => p.snedPhone.Contains(snedPhone));
            }
			// varchar
			var recipient = Request.Form["recipient"].FirstOrDefault();
            if (!string.IsNullOrEmpty(recipient))
            {
                where = where.And(p => p.recipient.Contains(recipient));
            }
			// varchar
			var receAddress = Request.Form["receAddress"].FirstOrDefault();
            if (!string.IsNullOrEmpty(receAddress))
            {
                where = where.And(p => p.receAddress.Contains(receAddress));
            }
			// varchar
			var recePhone = Request.Form["recePhone"].FirstOrDefault();
            if (!string.IsNullOrEmpty(recePhone))
            {
                where = where.And(p => p.recePhone.Contains(recePhone));
            }
			// int
			var carType = Request.Form["carType"].FirstOrDefault().ObjectToInt(0);
            if (carType > 0)
            {
                where = where.And(p => p.carType == carType);
            }
			// varchar
			var pay = Request.Form["pay"].FirstOrDefault();
            if (!string.IsNullOrEmpty(pay))
            {
                where = where.And(p => p.pay.Contains(pay));
            }
			// varchar
			var amount = Request.Form["amount"].FirstOrDefault();
            if (!string.IsNullOrEmpty(amount))
            {
                where = where.And(p => p.amount.Contains(amount));
            }
			// varbinary
			var settlementNum = Request.Form["settlementNum"].FirstOrDefault();
            if (!string.IsNullOrEmpty(settlementNum))
            {
                where = where.And(p => p.settlementNum.Contains(settlementNum));
            }
			// varchar
			var remark = Request.Form["remark"].FirstOrDefault();
            if (!string.IsNullOrEmpty(remark))
            {
                where = where.And(p => p.remark.Contains(remark));
            }
			// varchar
			var itemName = Request.Form["itemName"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemName))
            {
                where = where.And(p => p.itemName.Contains(itemName));
            }
			// varchar
			var itemType = Request.Form["itemType"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemType))
            {
                where = where.And(p => p.itemType.Contains(itemType));
            }
			// double
			var itemWeight = Request.Form["itemWeight"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemWeight))
            {
                where = where.And(p => p.itemWeight.ToString().Contains(itemWeight));
            }
			// double
			var itemVolume = Request.Form["itemVolume"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemVolume))
            {
                where = where.And(p => p.itemVolume.ToString().Contains(itemVolume));
            }
			// int
			var itemNum = Request.Form["itemNum"].FirstOrDefault().ObjectToInt(0);
            if (itemNum > 0)
            {
                where = where.And(p => p.itemNum == itemNum);
            }
			// varchar
			var itemPictures = Request.Form["itemPictures"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemPictures))
            {
                where = where.And(p => p.itemPictures.Contains(itemPictures));
            }
			// datetime
			var deliveryTime = Request.Form["deliveryTime"].FirstOrDefault();
            if (!string.IsNullOrEmpty(deliveryTime))
            {
                if (deliveryTime.Contains("到"))
                {
                    var dts = deliveryTime.Split("到");
                    var dtStart = dts[0].Trim().ObjectToDate();
                    where = where.And(p => p.deliveryTime > dtStart);
                    var dtEnd = dts[1].Trim().ObjectToDate();
                    where = where.And(p => p.deliveryTime < dtEnd);
                }
                else
                {
                    var dt = deliveryTime.ObjectToDate();
                    where = where.And(p => p.deliveryTime > dt);
                }
            }
			// bit
			var isDelete = Request.Form["isDelete"].FirstOrDefault();
            if (!string.IsNullOrEmpty(isDelete) && isDelete.ToLowerInvariant() == "true")
            {
                where = where.And(p => p.isDelete == true);
            }
            else if (!string.IsNullOrEmpty(isDelete) && isDelete.ToLowerInvariant() == "false")
            {
                where = where.And(p => p.isDelete == false);
            }
			// bit
			var isComfirm = Request.Form["isComfirm"].FirstOrDefault();
            if (!string.IsNullOrEmpty(isComfirm) && isComfirm.ToLowerInvariant() == "true")
            {
                where = where.And(p => p.isComfirm == true);
            }
            else if (!string.IsNullOrEmpty(isComfirm) && isComfirm.ToLowerInvariant() == "false")
            {
                where = where.And(p => p.isComfirm == false);
            }
			// int
			var state = Request.Form["state"].FirstOrDefault().ObjectToInt(0);
            if (state > 0)
            {
                where = where.And(p => p.state == state);
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
            var list = await _yl_ordersServices.QueryPageAsync(where, orderEx, orderBy, pageCurrent, pageSize, true);
            //返回数据
            jm.data = list;
            jm.code = 0;
            jm.count = list.TotalCount;
            jm.msg = "数据调用成功!";
            return jm;
        }
        #endregion

        #region 首页数据============================================================
        // POST: Api/yl_orders/GetIndex
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
        // POST: Api/yl_orders/GetCreate
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
        // POST: Api/yl_orders/DoCreate
        /// <summary>
        /// 创建提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("创建提交")]
        public async Task<AdminUiCallBack> DoCreate([FromBody]yl_orders entity)
        {
            var jm = new AdminUiCallBack();
            var result = await _yl_ordersServices.InsertAsync(entity);
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
        // POST: Api/yl_orders/GetEdit
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

            var model = await _yl_ordersServices.QueryByIdAsync(entity.id, false);
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
        // POST: Api/yl_orders/Edit
        /// <summary>
        /// 编辑提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("编辑提交")]
        public async Task<AdminUiCallBack> DoEdit([FromBody]yl_orders entity)
        {
            var jm = new AdminUiCallBack();
            var result = await _yl_ordersServices.UpdateAsync(entity);
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
        // POST: Api/yl_orders/DoDelete/10
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

            var model = await _yl_ordersServices.ExistsAsync(p => p.id == entity.id, true);
            if (!model)
            {
                jm.msg = GlobalConstVars.DataisNo;
				return jm;
            }
            var result = await _yl_ordersServices.DeleteByIdAsync(entity.id);
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
        // POST: Api/yl_orders/DoBatchDelete/10,11,20
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
            var result = await _yl_ordersServices.DeleteByIdsAsync(entity.id);
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
        // POST: Api/yl_orders/GetDetails/10
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

            var model = await _yl_ordersServices.QueryByIdAsync(entity.id, false);
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
        // POST: Api/yl_orders/SelectExportExcel/10
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
            var listModel = await _yl_ordersServices.QueryListByClauseAsync(p => entity.id.Contains(p.id), p => p.id, OrderByType.Asc, true);
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

            var cell9 = headerRow.CreateCell(9);
            cell9.SetCellValue("");
            cell9.CellStyle = headerStyle;
            mySheet.SetColumnWidth(9, 10 * 256);

            var cell10 = headerRow.CreateCell(10);
            cell10.SetCellValue("");
            cell10.CellStyle = headerStyle;
            mySheet.SetColumnWidth(10, 10 * 256);

            var cell11 = headerRow.CreateCell(11);
            cell11.SetCellValue("");
            cell11.CellStyle = headerStyle;
            mySheet.SetColumnWidth(11, 10 * 256);

            var cell12 = headerRow.CreateCell(12);
            cell12.SetCellValue("");
            cell12.CellStyle = headerStyle;
            mySheet.SetColumnWidth(12, 10 * 256);

            var cell13 = headerRow.CreateCell(13);
            cell13.SetCellValue("");
            cell13.CellStyle = headerStyle;
            mySheet.SetColumnWidth(13, 10 * 256);

            var cell14 = headerRow.CreateCell(14);
            cell14.SetCellValue("");
            cell14.CellStyle = headerStyle;
            mySheet.SetColumnWidth(14, 10 * 256);

            var cell15 = headerRow.CreateCell(15);
            cell15.SetCellValue("");
            cell15.CellStyle = headerStyle;
            mySheet.SetColumnWidth(15, 10 * 256);

            var cell16 = headerRow.CreateCell(16);
            cell16.SetCellValue("");
            cell16.CellStyle = headerStyle;
            mySheet.SetColumnWidth(16, 10 * 256);

            var cell17 = headerRow.CreateCell(17);
            cell17.SetCellValue("");
            cell17.CellStyle = headerStyle;
            mySheet.SetColumnWidth(17, 10 * 256);

            var cell18 = headerRow.CreateCell(18);
            cell18.SetCellValue("");
            cell18.CellStyle = headerStyle;
            mySheet.SetColumnWidth(18, 10 * 256);

            var cell19 = headerRow.CreateCell(19);
            cell19.SetCellValue("");
            cell19.CellStyle = headerStyle;
            mySheet.SetColumnWidth(19, 10 * 256);

            var cell20 = headerRow.CreateCell(20);
            cell20.SetCellValue("");
            cell20.CellStyle = headerStyle;
            mySheet.SetColumnWidth(20, 10 * 256);

            var cell21 = headerRow.CreateCell(21);
            cell21.SetCellValue("");
            cell21.CellStyle = headerStyle;
            mySheet.SetColumnWidth(21, 10 * 256);

            var cell22 = headerRow.CreateCell(22);
            cell22.SetCellValue("");
            cell22.CellStyle = headerStyle;
            mySheet.SetColumnWidth(22, 10 * 256);

            var cell23 = headerRow.CreateCell(23);
            cell23.SetCellValue("");
            cell23.CellStyle = headerStyle;
            mySheet.SetColumnWidth(23, 10 * 256);

            var cell24 = headerRow.CreateCell(24);
            cell24.SetCellValue("");
            cell24.CellStyle = headerStyle;
            mySheet.SetColumnWidth(24, 10 * 256);

            var cell25 = headerRow.CreateCell(25);
            cell25.SetCellValue("");
            cell25.CellStyle = headerStyle;
            mySheet.SetColumnWidth(25, 10 * 256);

            var cell26 = headerRow.CreateCell(26);
            cell26.SetCellValue("");
            cell26.CellStyle = headerStyle;
            mySheet.SetColumnWidth(26, 10 * 256);

            var cell27 = headerRow.CreateCell(27);
            cell27.SetCellValue("");
            cell27.CellStyle = headerStyle;
            mySheet.SetColumnWidth(27, 10 * 256);

            var cell28 = headerRow.CreateCell(28);
            cell28.SetCellValue("");
            cell28.CellStyle = headerStyle;
            mySheet.SetColumnWidth(28, 10 * 256);

            var cell29 = headerRow.CreateCell(29);
            cell29.SetCellValue("");
            cell29.CellStyle = headerStyle;
            mySheet.SetColumnWidth(29, 10 * 256);

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
                        rowTemp1.SetCellValue(listModel[i].number.ToString());
                        rowTemp1.CellStyle = commonCellStyle;

                    var rowTemp2 = rowTemp.CreateCell(2);
                        rowTemp2.SetCellValue(listModel[i].orderType.ToString());
                        rowTemp2.CellStyle = commonCellStyle;

                    var rowTemp3 = rowTemp.CreateCell(3);
                        rowTemp3.SetCellValue(listModel[i].userid.ToString());
                        rowTemp3.CellStyle = commonCellStyle;

                    var rowTemp4 = rowTemp.CreateCell(4);
                        rowTemp4.SetCellValue(listModel[i].driverid.ToString());
                        rowTemp4.CellStyle = commonCellStyle;

                    var rowTemp5 = rowTemp.CreateCell(5);
                        rowTemp5.SetCellValue(listModel[i].sneder.ToString());
                        rowTemp5.CellStyle = commonCellStyle;

                    var rowTemp6 = rowTemp.CreateCell(6);
                        rowTemp6.SetCellValue(listModel[i].sendAddress.ToString());
                        rowTemp6.CellStyle = commonCellStyle;

                    var rowTemp7 = rowTemp.CreateCell(7);
                        rowTemp7.SetCellValue(listModel[i].snedPhone.ToString());
                        rowTemp7.CellStyle = commonCellStyle;

                    var rowTemp8 = rowTemp.CreateCell(8);
                        rowTemp8.SetCellValue(listModel[i].recipient.ToString());
                        rowTemp8.CellStyle = commonCellStyle;

                    var rowTemp9 = rowTemp.CreateCell(9);
                        rowTemp9.SetCellValue(listModel[i].receAddress.ToString());
                        rowTemp9.CellStyle = commonCellStyle;

                    var rowTemp10 = rowTemp.CreateCell(10);
                        rowTemp10.SetCellValue(listModel[i].recePhone.ToString());
                        rowTemp10.CellStyle = commonCellStyle;

                    var rowTemp11 = rowTemp.CreateCell(11);
                        rowTemp11.SetCellValue(listModel[i].carType.ToString());
                        rowTemp11.CellStyle = commonCellStyle;

                    var rowTemp12 = rowTemp.CreateCell(12);
                        rowTemp12.SetCellValue(listModel[i].pay.ToString());
                        rowTemp12.CellStyle = commonCellStyle;

                    var rowTemp13 = rowTemp.CreateCell(13);
                        rowTemp13.SetCellValue(listModel[i].amount.ToString());
                        rowTemp13.CellStyle = commonCellStyle;

                    var rowTemp14 = rowTemp.CreateCell(14);
                        rowTemp14.SetCellValue(listModel[i].settlementNum.ToString());
                        rowTemp14.CellStyle = commonCellStyle;

                    var rowTemp15 = rowTemp.CreateCell(15);
                        rowTemp15.SetCellValue(listModel[i].remark.ToString());
                        rowTemp15.CellStyle = commonCellStyle;

                    var rowTemp16 = rowTemp.CreateCell(16);
                        rowTemp16.SetCellValue(listModel[i].itemName.ToString());
                        rowTemp16.CellStyle = commonCellStyle;

                    var rowTemp17 = rowTemp.CreateCell(17);
                        rowTemp17.SetCellValue(listModel[i].itemType.ToString());
                        rowTemp17.CellStyle = commonCellStyle;

                    var rowTemp18 = rowTemp.CreateCell(18);
                        rowTemp18.SetCellValue(listModel[i].itemWeight.ToString());
                        rowTemp18.CellStyle = commonCellStyle;

                    var rowTemp19 = rowTemp.CreateCell(19);
                        rowTemp19.SetCellValue(listModel[i].itemVolume.ToString());
                        rowTemp19.CellStyle = commonCellStyle;

                    var rowTemp20 = rowTemp.CreateCell(20);
                        rowTemp20.SetCellValue(listModel[i].itemNum.ToString());
                        rowTemp20.CellStyle = commonCellStyle;

                    var rowTemp21 = rowTemp.CreateCell(21);
                        rowTemp21.SetCellValue(listModel[i].itemPictures.ToString());
                        rowTemp21.CellStyle = commonCellStyle;

                    var rowTemp22 = rowTemp.CreateCell(22);
                        rowTemp22.SetCellValue(listModel[i].deliveryTime.ToString());
                        rowTemp22.CellStyle = commonCellStyle;

                    var rowTemp23 = rowTemp.CreateCell(23);
                        rowTemp23.SetCellValue(listModel[i].isDelete.ToString());
                        rowTemp23.CellStyle = commonCellStyle;

                    var rowTemp24 = rowTemp.CreateCell(24);
                        rowTemp24.SetCellValue(listModel[i].isComfirm.ToString());
                        rowTemp24.CellStyle = commonCellStyle;

                    var rowTemp25 = rowTemp.CreateCell(25);
                        rowTemp25.SetCellValue(listModel[i].state.ToString());
                        rowTemp25.CellStyle = commonCellStyle;

                    var rowTemp26 = rowTemp.CreateCell(26);
                        rowTemp26.SetCellValue(listModel[i].createor.ToString());
                        rowTemp26.CellStyle = commonCellStyle;

                    var rowTemp27 = rowTemp.CreateCell(27);
                        rowTemp27.SetCellValue(listModel[i].createTime.ToString());
                        rowTemp27.CellStyle = commonCellStyle;

                    var rowTemp28 = rowTemp.CreateCell(28);
                        rowTemp28.SetCellValue(listModel[i].modified.ToString());
                        rowTemp28.CellStyle = commonCellStyle;

                    var rowTemp29 = rowTemp.CreateCell(29);
                        rowTemp29.SetCellValue(listModel[i].modifyTime.ToString());
                        rowTemp29.CellStyle = commonCellStyle;

            }
            // 导出excel
            string webRootPath = _webHostEnvironment.WebRootPath;
            string tpath = "/files/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-yl_orders导出(选择结果).xls";
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
        // POST: Api/yl_orders/QueryExportExcel/10
        /// <summary>
        /// 查询导出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("查询导出")]
        public async Task<AdminUiCallBack> QueryExportExcel()
        {
            var jm = new AdminUiCallBack();

            var where = PredicateBuilder.True<yl_orders>();
                //查询筛选
			
			// int
			var id = Request.Form["id"].FirstOrDefault().ObjectToInt(0);
            if (id > 0)
            {
                where = where.And(p => p.id == id);
            }
			// varchar
			var number = Request.Form["number"].FirstOrDefault();
            if (!string.IsNullOrEmpty(number))
            {
                where = where.And(p => p.number.Contains(number));
            }
			// varchar
			var orderType = Request.Form["orderType"].FirstOrDefault();
            if (!string.IsNullOrEmpty(orderType))
            {
                where = where.And(p => p.orderType.Contains(orderType));
            }
			// int
			var userid = Request.Form["userid"].FirstOrDefault().ObjectToInt(0);
            if (userid > 0)
            {
                where = where.And(p => p.userid == userid);
            }
			// int
			var driverid = Request.Form["driverid"].FirstOrDefault().ObjectToInt(0);
            if (driverid > 0)
            {
                where = where.And(p => p.driverid == driverid);
            }
			// varchar
			var sneder = Request.Form["sneder"].FirstOrDefault();
            if (!string.IsNullOrEmpty(sneder))
            {
                where = where.And(p => p.sneder.Contains(sneder));
            }
			// varchar
			var sendAddress = Request.Form["sendAddress"].FirstOrDefault();
            if (!string.IsNullOrEmpty(sendAddress))
            {
                where = where.And(p => p.sendAddress.Contains(sendAddress));
            }
			// varchar
			var snedPhone = Request.Form["snedPhone"].FirstOrDefault();
            if (!string.IsNullOrEmpty(snedPhone))
            {
                where = where.And(p => p.snedPhone.Contains(snedPhone));
            }
			// varchar
			var recipient = Request.Form["recipient"].FirstOrDefault();
            if (!string.IsNullOrEmpty(recipient))
            {
                where = where.And(p => p.recipient.Contains(recipient));
            }
			// varchar
			var receAddress = Request.Form["receAddress"].FirstOrDefault();
            if (!string.IsNullOrEmpty(receAddress))
            {
                where = where.And(p => p.receAddress.Contains(receAddress));
            }
			// varchar
			var recePhone = Request.Form["recePhone"].FirstOrDefault();
            if (!string.IsNullOrEmpty(recePhone))
            {
                where = where.And(p => p.recePhone.Contains(recePhone));
            }
			// int
			var carType = Request.Form["carType"].FirstOrDefault().ObjectToInt(0);
            if (carType > 0)
            {
                where = where.And(p => p.carType == carType);
            }
			// varchar
			var pay = Request.Form["pay"].FirstOrDefault();
            if (!string.IsNullOrEmpty(pay))
            {
                where = where.And(p => p.pay.Contains(pay));
            }
			// varchar
			var amount = Request.Form["amount"].FirstOrDefault();
            if (!string.IsNullOrEmpty(amount))
            {
                where = where.And(p => p.amount.Contains(amount));
            }
			// varbinary
			var settlementNum = Request.Form["settlementNum"].FirstOrDefault();
            if (!string.IsNullOrEmpty(settlementNum))
            {
                where = where.And(p => p.settlementNum.Contains(settlementNum));
            }
			// varchar
			var remark = Request.Form["remark"].FirstOrDefault();
            if (!string.IsNullOrEmpty(remark))
            {
                where = where.And(p => p.remark.Contains(remark));
            }
			// varchar
			var itemName = Request.Form["itemName"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemName))
            {
                where = where.And(p => p.itemName.Contains(itemName));
            }
			// varchar
			var itemType = Request.Form["itemType"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemType))
            {
                where = where.And(p => p.itemType.Contains(itemType));
            }
			// double
			var itemWeight = Request.Form["itemWeight"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemWeight))
            {
                where = where.And(p => p.itemWeight.ToString().Contains(itemWeight));
            }
			// double
			var itemVolume = Request.Form["itemVolume"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemVolume))
            {
                where = where.And(p => p.itemVolume.ToString().Contains(itemVolume));
            }
			// int
			var itemNum = Request.Form["itemNum"].FirstOrDefault().ObjectToInt(0);
            if (itemNum > 0)
            {
                where = where.And(p => p.itemNum == itemNum);
            }
			// varchar
			var itemPictures = Request.Form["itemPictures"].FirstOrDefault();
            if (!string.IsNullOrEmpty(itemPictures))
            {
                where = where.And(p => p.itemPictures.Contains(itemPictures));
            }
			// datetime
			var deliveryTime = Request.Form["deliveryTime"].FirstOrDefault();
            if (!string.IsNullOrEmpty(deliveryTime))
            {
                var dt = deliveryTime.ObjectToDate();
                where = where.And(p => p.deliveryTime > dt);
            }
			// bit
			var isDelete = Request.Form["isDelete"].FirstOrDefault();
            if (!string.IsNullOrEmpty(isDelete) && isDelete.ToLowerInvariant() == "true")
            {
                where = where.And(p => p.isDelete == true);
            }
            else if (!string.IsNullOrEmpty(isDelete) && isDelete.ToLowerInvariant() == "false")
            {
                where = where.And(p => p.isDelete == false);
            }
			// bit
			var isComfirm = Request.Form["isComfirm"].FirstOrDefault();
            if (!string.IsNullOrEmpty(isComfirm) && isComfirm.ToLowerInvariant() == "true")
            {
                where = where.And(p => p.isComfirm == true);
            }
            else if (!string.IsNullOrEmpty(isComfirm) && isComfirm.ToLowerInvariant() == "false")
            {
                where = where.And(p => p.isComfirm == false);
            }
			// int
			var state = Request.Form["state"].FirstOrDefault().ObjectToInt(0);
            if (state > 0)
            {
                where = where.And(p => p.state == state);
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
            var listModel = await _yl_ordersServices.QueryListByClauseAsync(where, p => p.id, OrderByType.Asc, true);
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
			
            var cell9 = headerRow.CreateCell(9);
            cell9.SetCellValue("");
            cell9.CellStyle = headerStyle;
            mySheet.SetColumnWidth(9, 10 * 256);
			
            var cell10 = headerRow.CreateCell(10);
            cell10.SetCellValue("");
            cell10.CellStyle = headerStyle;
            mySheet.SetColumnWidth(10, 10 * 256);
			
            var cell11 = headerRow.CreateCell(11);
            cell11.SetCellValue("");
            cell11.CellStyle = headerStyle;
            mySheet.SetColumnWidth(11, 10 * 256);
			
            var cell12 = headerRow.CreateCell(12);
            cell12.SetCellValue("");
            cell12.CellStyle = headerStyle;
            mySheet.SetColumnWidth(12, 10 * 256);
			
            var cell13 = headerRow.CreateCell(13);
            cell13.SetCellValue("");
            cell13.CellStyle = headerStyle;
            mySheet.SetColumnWidth(13, 10 * 256);
			
            var cell14 = headerRow.CreateCell(14);
            cell14.SetCellValue("");
            cell14.CellStyle = headerStyle;
            mySheet.SetColumnWidth(14, 10 * 256);
			
            var cell15 = headerRow.CreateCell(15);
            cell15.SetCellValue("");
            cell15.CellStyle = headerStyle;
            mySheet.SetColumnWidth(15, 10 * 256);
			
            var cell16 = headerRow.CreateCell(16);
            cell16.SetCellValue("");
            cell16.CellStyle = headerStyle;
            mySheet.SetColumnWidth(16, 10 * 256);
			
            var cell17 = headerRow.CreateCell(17);
            cell17.SetCellValue("");
            cell17.CellStyle = headerStyle;
            mySheet.SetColumnWidth(17, 10 * 256);
			
            var cell18 = headerRow.CreateCell(18);
            cell18.SetCellValue("");
            cell18.CellStyle = headerStyle;
            mySheet.SetColumnWidth(18, 10 * 256);
			
            var cell19 = headerRow.CreateCell(19);
            cell19.SetCellValue("");
            cell19.CellStyle = headerStyle;
            mySheet.SetColumnWidth(19, 10 * 256);
			
            var cell20 = headerRow.CreateCell(20);
            cell20.SetCellValue("");
            cell20.CellStyle = headerStyle;
            mySheet.SetColumnWidth(20, 10 * 256);
			
            var cell21 = headerRow.CreateCell(21);
            cell21.SetCellValue("");
            cell21.CellStyle = headerStyle;
            mySheet.SetColumnWidth(21, 10 * 256);
			
            var cell22 = headerRow.CreateCell(22);
            cell22.SetCellValue("");
            cell22.CellStyle = headerStyle;
            mySheet.SetColumnWidth(22, 10 * 256);
			
            var cell23 = headerRow.CreateCell(23);
            cell23.SetCellValue("");
            cell23.CellStyle = headerStyle;
            mySheet.SetColumnWidth(23, 10 * 256);
			
            var cell24 = headerRow.CreateCell(24);
            cell24.SetCellValue("");
            cell24.CellStyle = headerStyle;
            mySheet.SetColumnWidth(24, 10 * 256);
			
            var cell25 = headerRow.CreateCell(25);
            cell25.SetCellValue("");
            cell25.CellStyle = headerStyle;
            mySheet.SetColumnWidth(25, 10 * 256);
			
            var cell26 = headerRow.CreateCell(26);
            cell26.SetCellValue("");
            cell26.CellStyle = headerStyle;
            mySheet.SetColumnWidth(26, 10 * 256);
			
            var cell27 = headerRow.CreateCell(27);
            cell27.SetCellValue("");
            cell27.CellStyle = headerStyle;
            mySheet.SetColumnWidth(27, 10 * 256);
			
            var cell28 = headerRow.CreateCell(28);
            cell28.SetCellValue("");
            cell28.CellStyle = headerStyle;
            mySheet.SetColumnWidth(28, 10 * 256);
			
            var cell29 = headerRow.CreateCell(29);
            cell29.SetCellValue("");
            cell29.CellStyle = headerStyle;
            mySheet.SetColumnWidth(29, 10 * 256);
			

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
            rowTemp1.SetCellValue(listModel[i].number.ToString());
            rowTemp1.CellStyle = commonCellStyle;



            var rowTemp2 = rowTemp.CreateCell(2);
            rowTemp2.SetCellValue(listModel[i].orderType.ToString());
            rowTemp2.CellStyle = commonCellStyle;



            var rowTemp3 = rowTemp.CreateCell(3);
            rowTemp3.SetCellValue(listModel[i].userid.ToString());
            rowTemp3.CellStyle = commonCellStyle;



            var rowTemp4 = rowTemp.CreateCell(4);
            rowTemp4.SetCellValue(listModel[i].driverid.ToString());
            rowTemp4.CellStyle = commonCellStyle;



            var rowTemp5 = rowTemp.CreateCell(5);
            rowTemp5.SetCellValue(listModel[i].sneder.ToString());
            rowTemp5.CellStyle = commonCellStyle;



            var rowTemp6 = rowTemp.CreateCell(6);
            rowTemp6.SetCellValue(listModel[i].sendAddress.ToString());
            rowTemp6.CellStyle = commonCellStyle;



            var rowTemp7 = rowTemp.CreateCell(7);
            rowTemp7.SetCellValue(listModel[i].snedPhone.ToString());
            rowTemp7.CellStyle = commonCellStyle;



            var rowTemp8 = rowTemp.CreateCell(8);
            rowTemp8.SetCellValue(listModel[i].recipient.ToString());
            rowTemp8.CellStyle = commonCellStyle;



            var rowTemp9 = rowTemp.CreateCell(9);
            rowTemp9.SetCellValue(listModel[i].receAddress.ToString());
            rowTemp9.CellStyle = commonCellStyle;



            var rowTemp10 = rowTemp.CreateCell(10);
            rowTemp10.SetCellValue(listModel[i].recePhone.ToString());
            rowTemp10.CellStyle = commonCellStyle;



            var rowTemp11 = rowTemp.CreateCell(11);
            rowTemp11.SetCellValue(listModel[i].carType.ToString());
            rowTemp11.CellStyle = commonCellStyle;



            var rowTemp12 = rowTemp.CreateCell(12);
            rowTemp12.SetCellValue(listModel[i].pay.ToString());
            rowTemp12.CellStyle = commonCellStyle;



            var rowTemp13 = rowTemp.CreateCell(13);
            rowTemp13.SetCellValue(listModel[i].amount.ToString());
            rowTemp13.CellStyle = commonCellStyle;



            var rowTemp14 = rowTemp.CreateCell(14);
            rowTemp14.SetCellValue(listModel[i].settlementNum.ToString());
            rowTemp14.CellStyle = commonCellStyle;



            var rowTemp15 = rowTemp.CreateCell(15);
            rowTemp15.SetCellValue(listModel[i].remark.ToString());
            rowTemp15.CellStyle = commonCellStyle;



            var rowTemp16 = rowTemp.CreateCell(16);
            rowTemp16.SetCellValue(listModel[i].itemName.ToString());
            rowTemp16.CellStyle = commonCellStyle;



            var rowTemp17 = rowTemp.CreateCell(17);
            rowTemp17.SetCellValue(listModel[i].itemType.ToString());
            rowTemp17.CellStyle = commonCellStyle;



            var rowTemp18 = rowTemp.CreateCell(18);
            rowTemp18.SetCellValue(listModel[i].itemWeight.ToString());
            rowTemp18.CellStyle = commonCellStyle;



            var rowTemp19 = rowTemp.CreateCell(19);
            rowTemp19.SetCellValue(listModel[i].itemVolume.ToString());
            rowTemp19.CellStyle = commonCellStyle;



            var rowTemp20 = rowTemp.CreateCell(20);
            rowTemp20.SetCellValue(listModel[i].itemNum.ToString());
            rowTemp20.CellStyle = commonCellStyle;



            var rowTemp21 = rowTemp.CreateCell(21);
            rowTemp21.SetCellValue(listModel[i].itemPictures.ToString());
            rowTemp21.CellStyle = commonCellStyle;



            var rowTemp22 = rowTemp.CreateCell(22);
            rowTemp22.SetCellValue(listModel[i].deliveryTime.ToString());
            rowTemp22.CellStyle = commonCellStyle;



            var rowTemp23 = rowTemp.CreateCell(23);
            rowTemp23.SetCellValue(listModel[i].isDelete.ToString());
            rowTemp23.CellStyle = commonCellStyle;



            var rowTemp24 = rowTemp.CreateCell(24);
            rowTemp24.SetCellValue(listModel[i].isComfirm.ToString());
            rowTemp24.CellStyle = commonCellStyle;



            var rowTemp25 = rowTemp.CreateCell(25);
            rowTemp25.SetCellValue(listModel[i].state.ToString());
            rowTemp25.CellStyle = commonCellStyle;



            var rowTemp26 = rowTemp.CreateCell(26);
            rowTemp26.SetCellValue(listModel[i].createor.ToString());
            rowTemp26.CellStyle = commonCellStyle;



            var rowTemp27 = rowTemp.CreateCell(27);
            rowTemp27.SetCellValue(listModel[i].createTime.ToString());
            rowTemp27.CellStyle = commonCellStyle;



            var rowTemp28 = rowTemp.CreateCell(28);
            rowTemp28.SetCellValue(listModel[i].modified.ToString());
            rowTemp28.CellStyle = commonCellStyle;



            var rowTemp29 = rowTemp.CreateCell(29);
            rowTemp29.SetCellValue(listModel[i].modifyTime.ToString());
            rowTemp29.CellStyle = commonCellStyle;


            }
            // 写入到excel
            string webRootPath = _webHostEnvironment.WebRootPath;
            string tpath = "/files/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-yl_orders导出(查询结果).xls";
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

        
        #region 设置============================================================
        // POST: Api/yl_orders/DoSetisDelete/10
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("设置")]
        public async Task<AdminUiCallBack> DoSetisDelete([FromBody]FMUpdateBoolDataByIntId entity)
        {
            var jm = new AdminUiCallBack();

            var oldModel = await _yl_ordersServices.QueryByIdAsync(entity.id, false);
            if (oldModel == null)
            {
                jm.msg = "不存在此信息";
                return jm;
            }
            oldModel.isDelete = (bool)entity.data;

            var bl = await _yl_ordersServices.UpdateAsync(p => new yl_orders() { isDelete = oldModel.isDelete }, p => p.id == oldModel.id);
            jm.code = bl ? 0 : 1;
            jm.msg = bl ? GlobalConstVars.EditSuccess : GlobalConstVars.EditFailure;

            return jm;
		}
        #endregion
        
        #region 设置============================================================
        // POST: Api/yl_orders/DoSetisComfirm/10
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("设置")]
        public async Task<AdminUiCallBack> DoSetisComfirm([FromBody]FMUpdateBoolDataByIntId entity)
        {
            var jm = new AdminUiCallBack();

            var oldModel = await _yl_ordersServices.QueryByIdAsync(entity.id, false);
            if (oldModel == null)
            {
                jm.msg = "不存在此信息";
                return jm;
            }
            oldModel.isComfirm = (bool)entity.data;

            var bl = await _yl_ordersServices.UpdateAsync(p => new yl_orders() { isComfirm = oldModel.isComfirm }, p => p.id == oldModel.id);
            jm.code = bl ? 0 : 1;
            jm.msg = bl ? GlobalConstVars.EditSuccess : GlobalConstVars.EditFailure;

            return jm;
		}
        #endregion
        

    }
}
