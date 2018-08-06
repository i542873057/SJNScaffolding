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
using Abp.Domain.Repositories;
using Cargo.Domain.Plat.GWareHouseInfos.Dto;
using Cargo.Usual;

namespace Cargo.Domain.Plat.GWareHouseInfos
{
    public class GWareHouseInfoAppService : UsualCrudAppService<GWareHouseInfo, GWareHouseInfoListDto, GWareHouseInfoSearchDto, GWareHouseInfoInputDto, long>, IGWareHouseInfoAppService
    {
        private readonly IRepository<GWareHouseInfo, long> _iGWareHouseInfoRepository;

        public GWareHouseInfoAppService(IRepository<GWareHouseInfo, long> iGWareHouseInfoRepository) : base(iGWareHouseInfoRepository)
        {
            _iGWareHouseInfoRepository = iGWareHouseInfoRepository;
        }

    }
}

