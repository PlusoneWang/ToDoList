/**
 * 傳入Form物件，顯示詢問內容，並在使用者確定後執行Submit，否則取消動作。 
 * 目前預設當submit動作執行後，將阻擋二次submit。
 * 相容於Jquery Validation，當該套件已載入且表單驗證不通過時改由該套件阻擋。
 * @example
 * FormConfirm($('form'),'即將送出，確定?');
 * @param {JQueryObject} form 要執行Submit動作的Form
 * @param {String} confirmContext 要顯示的詢問內容
 * @returns {void} 
 */
function FormConfirm(form, confirmContext) {
    /*
    * 目前可用的Confirm屬性值: default(瀏覽器預設)、。
    * 例:
    * <form></form> => 可執行，以bootbox樣式彈出詢問視窗
    * <form confirm="bootbox"></form> => 可執行，以bootbox樣式彈出詢問視窗
    * <form confirm="simple"></form> => 可執行，彈出瀏覽器預設詢問視窗
    * <form confirm></form> => 什麼都不會發生。
    */

    if (typeof form === 'undefined' || form === null || !form.is('form')) {
        return;
    }

    // 重設詢問內容
    if (typeof confirmContext !== 'string' || confirmContext.length === 0) {
        confirmContext = '確定?';
    }

    if (!form.is('[confirm]') || form.attr('confirm') === 'default') {
        if (confirm(confirmContext)) {
            form.submit();
            if ($.validator) {
                if (form.valid()) {
                    form.on('submit', function () {
                        return false;
                    });
                }
            } else {
                form.on('submit', function () {
                    return false;
                });
            }
        }
    } else if (FormConfirm[form.attr('confirm')] && typeof FormConfirm[form.attr('confirm')] === "function") {
        FormConfirm[form.attr('confirm')](confirmContext)
            .then(function (result) {
                if (result === true) {
                    form.submit();
                    if ($.validator) {
                        if (form.valid()) {
                            form.on('submit',
                                function () {
                                    return false;
                                });
                        }
                    } else {
                        form.on('submit',
                            function () {
                                return false;
                            });
                    }
                }
            });
    } else {
        console.warn("confirm設定值沒有相對應的處理函式");
    }
}

FormConfirm.sweetalert = function (question) {
    return swal({
        title: question,
        type: "question",
        showCancelButton: true,
        confirmButtonColor: "#007bff",
        confirmButtonText: "確定",
        cancelButtonText: "取消"
    }).then((ans) => {
        return new window.Promise(function (resolve, reject) {
            resolve(ans.value);
        });
    });
}


// TODO 讀Code，改Code
function resolveReferences(json) {
    if (typeof json === 'string')
        json = JSON.parse(json);

    var byid = {}, // all objects by id
        refs = []; // references to objects that could not be resolved
    json = (function recurse(obj, prop, parent) {
        if (typeof obj !== 'object' || !obj) // a primitive value
            return obj;
        if (Object.prototype.toString.call(obj) === '[object Array]') {
            for (var i = 0; i < obj.length; i++)
            // check also if the array element is not a primitive value
                if (typeof obj[i] !== 'object' || !obj[i]) // a primitive value
                    continue;
                else if ("$ref" in obj[i])
                    obj[i] = recurse(obj[i], i, obj);
                else
                    obj[i] = recurse(obj[i], prop, obj);
            return obj;
        }
        if ("$ref" in obj) { // a reference
            var ref = obj.$ref;
            if (ref in byid)
                return byid[ref];
            // else we have to make it lazy:
            refs.push([parent, prop, ref]);
            return;
        } else if ("$id" in obj) {
            var id = obj.$id;
            delete obj.$id;
            if ("$values" in obj) // an array
                obj = obj.$values.map(recurse);
            else // a plain object
                for (var prop in obj)
                    obj[prop] = recurse(obj[prop], prop, obj);
            byid[id] = obj;
        }
        return obj;
    })(json); // run it!

    for (var i = 0; i < refs.length; i++) { // resolve previously unknown references
        var ref = refs[i];
        ref[0][ref[1]] = byid[ref[2]];
        // Notice that this throws if you put in a reference at top-level
    }
    return json;
}
