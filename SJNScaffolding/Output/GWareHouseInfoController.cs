//=============================================================
// 创建人:               邵佳楠   
// 创建时间:            2018年08月06日 09时07分05秒
// 邮箱：             1542873057@qq.com 
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cargo.Domain.Plat.GWareHouseInfos;
using Cargo.Domain.Plat.GWareHouseInfos.Dto;
using Cargo.Web.Controllers;

namespace Cargo.Web.Areas.Plat.Controllers
{
    public class GWareHouseInfoController : CargoControllerBase
    {
        // GET: Plat/GWareHouseInfo


        private readonly IGWareHouseInfoAppService _iGWareHouseInfoAppService;

        public GWareHouseInfoController(IGWareHouseInfoAppService iGWareHouseInfoAppService)
        {
            _iGWareHouseInfoAppService = iGWareHouseInfoAppService;
        }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新增编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrUpdateModal()
        {
            return View();
        }

        #region 数据
        /// <summary>
        /// 根据ID获取编辑数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetInfoForEdit(int id)
        {
            var input = await _iGWareHouseInfoAppService.GetInfoForEdit(id);

            return Json(input);
        }

        /// <summary>
        /// 根据条件查询对应列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult GetGridByCondition(GWareHouseInfoSearchDto input)
        {
            var gridData = _iGWareHouseInfoAppService.GetGridByCondition(input);
            return Json(gridData);
        }
        #endregion
    }
}
