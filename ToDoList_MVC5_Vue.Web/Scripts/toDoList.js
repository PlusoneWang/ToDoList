let app = new Vue({
    el: "#app",

    data: {
        collapsed: false, // 控制左側欄收合
        isOnSearch: false, // 控制搜尋欄及按鈕的顯示狀態
        searchText: "", // 搜尋字串
        toDoLists: [], // 待辦清單
        dragInfo: {
            dragType: null, // 當前的拖曳類型
            currentDrag: null,
        },
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
                if (data.Success !== true) {
                    swal(data.Message);
                    return;
                }
                data.Data.forEach((list) => {
                    list.sideClass = "";
                });
                this.toDoLists = data.Data;
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

    methods: {
        // 切換左側欄收合
        toggleCollapsed() {
            this.collapsed = !this.collapsed;
        },

        // 設定為正在搜尋
        setOnSearch() {
            this.isOnSearch = true;
        },

        // 清除搜尋
        setClearSearch() {
            this.isOnSearch = false;
            this.searchText = "";
        },

        // 拖曳資訊初始化
        listDragStart(event, list) {
            this.dragInfo.dragType = 'list';
            this.dragInfo.currentDrag = list;
        },

        // 重設拖曳資訊
        listDragEnd(event, list) {
            this.dragInfo.dragType = null;
            // TODO 重設當前拖曳類型、被拖曳的物件資訊
            // 如果目標容器有效，drop事件會先發生，但仍需注意拖曳物件資訊被重設後，drop的處理程序是否依然正常
            // 參考: DataTransfer.dropEffect，當此值為move時表示拖曳有效，重設的動作改為由drop物件處理
        },

        // 設定drag目標容器
        listDragOver(event, list) {
            switch (this.dragInfo.dragType) {
                case "list":
                    {
                        event.preventDefault();
                        const rect = event.currentTarget.getBoundingClientRect();
                        if (event.clientY - rect.y < 12) {
                            list.side = "top";
                        } else if (rect.y + rect.height - event.clientY < 12) {
                            list.side = "bottom";
                        } else {
                            list.side = "both";
                        }
                    }

                    list.sideClass = `listondragover-${list.side}`;
                    break;
            }
        },

        // 移除list.side property
        listDragLeave(event, list) {
            list.side = null;
            list.sideClass = null;
        },

        listDrop(event, list) {
            if (list.side !== null) {
                list.sideClass = null;
                switch (list.side) {
                    case "top": {
                        if (this.dragInfo.currentDrag === list)
                            return;
                        const currentDragIndex = this.toDoLists.findIndex((element) => element === this.dragInfo.currentDrag);
                        const currentDrag = this.toDoLists.splice(currentDragIndex, 1);
                        const insertIndex = this.toDoLists.findIndex((element) => element === list);
                        this.toDoLists.splice(insertIndex, 0, currentDrag[0]);
                        break;
                    }
                    case "bottom": {
                        if (this.dragInfo.currentDrag === list)
                            return;
                        const currentDragIndex = this.toDoLists.findIndex((element) => element === this.dragInfo.currentDrag);
                        const currentDrag = this.toDoLists.splice(currentDragIndex, 1);
                        const insertIndex = this.toDoLists.findIndex((element) => element === list);
                        this.toDoLists.splice(insertIndex + 1, 0, currentDrag[0]);
                        break;
                    }
                    case "both": {
                        console.log("both");
                        break;
                    }
                }
            }
            // TODO 
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
