/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:46
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public partial class yl_orders
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public yl_orders()
        {
        }
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "id")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 id  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "订单号")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String number  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "订单类型")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String orderType  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "用户ID（关联用户表）")]
		
        
        
        
        
        public System.Int32? userid  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "司机id（关联司机表）")]
		
        
        
        
        
        public System.Int32? driverid  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "发件人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String sneder  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "发件地址")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String sendAddress  { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "发件经度")]



        public double sendLat { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "发件纬度")]



        public double sendLng { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "发件人手机号")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String snedPhone  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "收件人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String recipient  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "收件地址")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String receAddress  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "收件经度")]



        public double receLat { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "收件纬度")]



        public double receLng { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "收件人手机号")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String recePhone  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "车型")]
		
        
        
        
        
        public System.Int32? carType  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "支付方式（1.寄付，2.到付）")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String pay  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "金额")]
		
        
        
        
        public double amount  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "月结账号")]





        public System.String settlementNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "附件")]



        [StringLength(maximumLength: 500, ErrorMessage = "{0}不能超过{1}字")]
        public System.String annexes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "备注")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String remark  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "物品名称")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String itemName  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "物品类型")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String itemType  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "物品重量")]
		
        
        
        public double itemWeight { get;set; }



        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "物品体积")]
		
        
        
        public double itemVolume { get; set; }  



        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "物品数量")]
		
        
        
        
        
        public System.Int32? itemNum  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "物品图片")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String itemPictures  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "送达时间")]
		
        
        
        
        
        public System.DateTime? deliveryTime  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "是否删除")]
		
        
        
        
        
        public System.Boolean? isDelete  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "是否确认下单")]
		
        
        
        
        
        public System.Boolean? isComfirm  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "是否已开发票")]




        public System.Boolean? isInvoice { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "订单状态")]




        public System.Int32? state  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "创建人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String creator  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "创建时间")]
		
        
        
        
        
        public System.DateTime? createTime  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "修改人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String modified  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "修改时间")]
		
        
        
        
        
        public System.DateTime? modifyTime  { get; set; }
        
		
    }
}
