//=============================================================
// 创建人:               邵佳楠   
// 创建时间:            2018年08月06日 09时07分05秒
// 邮箱：             1542873057@qq.com 
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cargo.CoreModel.Dto;

namespace Cargo.Domain.Plat.GWareHouseInfos.Dto
{
    public class GWareHouseInfoSearchDto : PageDto
    {
	     /// <summary>
/// 仓库名称（必填项）
/// </summary>
public string StoreName { get; set; }
/// <summary>
/// 联系人（必填项）
/// </summary>
public string LinkMan { get; set; }
/// <summary>
/// 联系方式（必填项）
/// </summary>
public string ContactNum { get; set; }
/// <summary>
/// 地址：省（必填项）
/// </summary>
public string Province { get; set; }
/// <summary>
/// 地址：市（必填项）
/// </summary>
public string City { get; set; }
/// <summary>
/// 地址：详细地址（必填项）
/// </summary>
public string RegAddress { get; set; }
/// <summary>
/// 总库容，单位立方，保留1位小数
/// </summary>
public decimal? StoreSize { get; set; }
/// <summary>
/// 仓库库体：长，单位米，小数精确到2位
/// </summary>
public decimal? StoreLength { get; set; }
/// <summary>
/// 仓库库体：宽，单位米，小数精确到2位
/// </summary>
public decimal? StoreWide { get; set; }
/// <summary>
/// 仓库库体：高，单位米，小数精确到2位
/// </summary>
public decimal? StoreHigth { get; set; }
/// <summary>
/// 出货方式（多选，逗号分隔：人工其他）
/// </summary>
public string ShipmentMode { get; set; }
/// <summary>
/// 月台数量
/// </summary>
public int? Platform { get; set; }
/// <summary>
/// 月台尺寸（长，单位米，小数精确到2位）
/// </summary>
public decimal? PlatformLen { get; set; }
/// <summary>
/// 月台尺寸（宽，单位米，小数精确到2位）
/// </summary>
public decimal? PlatformWide { get; set; }
/// <summary>
/// 月台尺寸（高，单位米，小数精确到2位）
/// </summary>
public decimal? PlatformHight { get; set; }
/// <summary>
/// 投产日期
/// </summary>
public DateTime? UseDate { get; set; }
/// <summary>
/// 仓库类型（单选，1-室外堆场；2库）项）
/// </summary>
public int? WareHouseType { get; set; }
/// <summary>
/// 建筑结构（单选，钢结构、混凝土、其他）
/// </summary>
public string BuildingStruct { get; set; }
/// <summary>
/// 柱网跨度
/// </summary>
public string ColNetSpan { get; set; }
/// <summary>
/// 建筑层高，单位米，保留1位小数
/// </summary>
public decimal? FloorHeight { get; set; }
/// <summary>
/// 结构承重
/// </summary>
public string StructLoad { get; set; }
/// <summary>
/// 库内设备（多选，、货架、托盘、分拣线）
/// </summary>
public string Devices { get; set; }
/// <summary>
/// 土地产权证（图片）
/// </summary>
public string OwnerShip { get; set; }
/// <summary>
/// 防火等级（单选：B1、B2、A1）
/// </summary>
public string FireRate { get; set; }
/// <summary>
/// 仓库照片（图片）
/// </summary>
public string WareHouseImages { get; set; }
/// <summary>
/// 备注
/// </summary>
public string Remark { get; set; }

    }
}

