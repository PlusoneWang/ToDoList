let app = new Vue({
    el: "#app",

    data: {
        collapsed: false, // 控制左側欄收合
        isOnSearch: false, // 控制搜尋欄及按鈕的顯示狀態
        searchText: "", // 搜尋字串
        toDoLists: [],
    },

    // 自定義指令
    directives: {
        focus: {
            update(el, { value }) {
                if (value) {
                    el.focus();
                }
            }
        }
    },

    created() {
        axios.get(Router.action("ToDoList", "GetListAndFolders"))
            .then(function (response) {
                const data = response.data;
                console.log(data);
                if (data.Success !== true) {
                    swal(data.Message);
                    return;
                }
                this.toDoLists = data.Data;
                // TODO set list to this.toDoLists
            }.bind(this)).catch(function (error) {
                if (error.response) {
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                } else if (error.request) {
                    console.log(error.request);
                } else {
                    console.log('Error', error.message);
                }
                console.log(error.config);
            }.bind(this));
    },

    updated() {
        feather.replace();
    },

    methods: {
        // 切換左側欄收合
        toggleCollapsed() {
            this.collapsed = !this.collapsed;
        },

        // 設定為搜尋
        setOnSearch() {
            this.isOnSearch = true;
        },

        // 清除搜尋
        setClearSearch() {
            this.isOnSearch = false;
            this.searchText = "";
        },

        // 新增待辦清單
        createList() {
            swal({
                title: '新增待辦清單',
                input: 'text',
                inputAttributes: {
                    autocapitalize: 'off',
                    maxlength: "255",
                },
                inputPlaceholder: "清單名稱",
                showCancelButton: true,
                cancelButtonText: "取消",
                confirmButtonText: '儲存',
                showLoaderOnConfirm: true,
                preConfirm: (listName) => {
                    return axios.post(Router.action("ToDoList", "CreateList"), { listName })
                        .then(function (response) {
                            const data = response.data;
                            console.log(data);
                            
                            // TODO update vue.data with data
                        })
                        .catch(function (error) {
                            if (error.response) {
                                console.log(error.response.data);
                                console.log(error.response.status);
                                console.log(error.response.headers);
                            } else if (error.request) {
                                console.log(error.request);
                            } else {
                                console.log('Error', error.message);
                            }
                            console.log(error.config);
                        });
                },
                allowOutsideClick: () => !swal.isLoading()
            });
        }
    }
});