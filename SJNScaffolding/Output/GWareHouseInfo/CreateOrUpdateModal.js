var editUI = {
    initCombobox: function () {
        var comboboxControl = [];

        $.each(comboboxControl, function (i, v) {
            $('#' + v).combobox({
                url: com.baseUrl + '/baseItem/GetComBoJson?enCode=' + v
            });
        });
    },
    initUpload: function () {
        //$("#OwnerShip").powerWebUpload({
        //   auto: false,
        //    uploadType: 'img'
        //});
        //$("#WareHouseImages").powerWebUpload({
        //    auto: false,
        //   uploadType: 'img'
        //});
    },
    setForm: function (id) {
        editUI.initCombobox();
        editUI.initUpload();
        com.setForm(id, function (jsonData) {
            //com.loadCityPicker($("#Province"), jsonData.Province, jsonData.City, jsonData.Area);
            //webuploader.loadFile({
            //    elem: '#OwnerShip',
            //    rows: jsonData['OwnerShipOutput']
            //});
            //webuploader.loadFile({
            //    elem: '#WareHouseImages',
            //   rows: jsonData['WareHouseImagesOutput']
            //});
        });
    }
}
