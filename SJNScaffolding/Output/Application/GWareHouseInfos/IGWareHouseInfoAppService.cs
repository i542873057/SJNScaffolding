//=============================================================
// 创建人:               邵佳楠   
// 创建时间:            2018年08月06日 09时07分05秒
// 邮箱：             1542873057@qq.com 
//==============================================================

using Cargo.Domain.Plat.GWareHouseInfos.Dto;
using Cargo.Usual;

namespace Cargo.Domain.Plat.GWareHouseInfos
{
    public interface IGWareHouseInfoAppService : IUsualCrudAppService<GWareHouseInfoListDto, GWareHouseInfoSearchDto, GWareHouseInfoInputDto, long>
    {
        
    }
}
