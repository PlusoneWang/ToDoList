let app = new Vue({
    el: "#app",

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

    created() {
        axios.get(Router.action("ToDoList", "GetListAndFolders"))
            .then(function (response) {
                const data = response.data;
                if (data.Success !== true) {
                    swal(data.Message);
                    return;
                }

                this.toDoLists = this.refactoredList(data.Data);
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
        toggleSidebarCollapsed() {
            this.collapsed = !this.collapsed;
        },

        // 切換資料夾收合
        toggleFolderCollapsed(folder) {
            folder.isCollapsed = !folder.isCollapsed;
        },

        // 取得資料夾ui標籤的高度(用於動畫)
        folderUlHeight(folder) {
            return `${folder.lists.length * 40}px`;
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
            this.dragInfo.dragType = list.type;
            this.dragInfo.currentDrag = list;
        },

        // 重設拖曳資訊
        listDragEnd(event, list) {
            if (event.dataTransfer.dropEffect !== "move") {
                this.dragInfo.dragType = null;
                this.dragInfo.currentDrag = null;
            }
        },

        // 設定drag目標容器
        listDragOver(event, list) {
            switch (list.type) {
                case "folder": {
                    switch (this.dragInfo.dragType) {
                        case "folder":
                        case "list":
                        case "subList": {
                            const rect = event.currentTarget.getBoundingClientRect();
                            if (event.clientY - rect.y < 12) {
                                event.preventDefault();
                                list.side = "top";
                                list.sideClass = `listondragover-${list.side}`;
                                return;
                            } else if (rect.y + rect.height - event.clientY < 12) {
                                event.preventDefault();
                                list.side = "bottom";
                                list.sideClass = `listondragover-${list.side}`;
                                return;
                            }

                            list.side = null;
                            list.sideClass = null;
                            return;
                        }
                    }
                    break;
                }

                case "list": {
                    switch (this.dragInfo.dragType) {
                        case "folder": {
                            const rect = event.currentTarget.getBoundingClientRect();
                            if (event.clientY - rect.y < 12) {
                                event.preventDefault();
                                list.side = "top";
                                list.sideClass = `listondragover-${list.side}`;
                                return;
                            } else if (rect.y + rect.height - event.clientY < 12) {
                                event.preventDefault();
                                list.side = "bottom";
                                list.sideClass = `listondragover-${list.side}`;
                                return;
                            }

                            list.side = null;
                            list.sideClass = null;
                            return;
                        }

                        case "list":
                        case "subList": {
                            event.preventDefault();
                            const rect = event.currentTarget.getBoundingClientRect();
                            if (event.clientY - rect.y < 12) {
                                list.side = "top";
                            } else if (rect.y + rect.height - event.clientY < 12) {
                                list.side = "bottom";
                            } else {
                                list.side = "center";
                            }

                            list.sideClass = `listondragover-${list.side}`;
                            return;
                        }
                    }
                }

                case "subList": {
                    switch (this.dragInfo.dragType) {
                        case "list":
                        case "subList": {
                            const rect = event.currentTarget.getBoundingClientRect();
                            if (event.clientY - rect.y < 12) {
                                event.preventDefault();
                                list.side = "top";
                                list.sideClass = `listondragover-${list.side}`;
                                return;
                            } else if (rect.y + rect.height - event.clientY < 12) {
                                event.preventDefault();
                                list.side = "bottom";
                                list.sideClass = `listondragover-${list.side}`;
                                return;
                            }
                            list.side = null;
                            list.sideClass = null;
                            return;
                        }
                    }
                }
            }
        },

        // 移除list.side property
        listDragLeave(event, list) {
            list.side = null;
            list.sideClass = null;
        },

        // 執行放置動作
        listDrop(event, list) {
            list.sideClass = null;
            const category = `${this.dragInfo.dragType}-dragto-${list.type}-${list.side}`;
            switch (category) {
                case "list-dragto-folder-top":
                case "list-dragto-folder-bottom":
                case "list-dragto-list-top":
                case "list-dragto-list-bottom":
                case "folder-dragto-folder-top":
                case "folder-dragto-folder-bottom":
                case "folder-dragto-list-top":
                case "folder-dragto-list-bottom": {
                    if (this.dragInfo.currentDrag === list) {
                        break;
                    }
                    if (list.side === "top") {
                        this.insertBefore(this.toDoLists, this.dragInfo.currentDrag, list);
                    } else {
                        this.insertAfter(this.toDoLists, this.dragInfo.currentDrag, list);
                    }
                    this.resortToDoLists();
                    break;
                }
            }

            list.side = null;
            this.dragInfo.dragType = null;
            this.dragInfo.currentDrag = null;
        },

        // 重構清單陣列
        refactoredList(sourceData) {
            let lists = [];
            for (let list of sourceData.Lists) {
                list.sideClass = "";
                if (list.FolderId === null) {
                    list.type = "list";
                    lists.push(list);
                    continue;
                }
                list.type = "subList";
                const folderIndex = lists.findIndex(listObj => listObj.Id === list.FolderId);
                if (folderIndex === -1) {
                    const folder = sourceData.Folders.find((folder) => folder.Id === list.FolderId);
                    folder.type = "folder";
                    folder.isFolder = true;
                    folder.isCollapsed = true;
                    folder.sideClass = "";
                    folder.lists = [list];
                    lists.push(folder);
                } else {
                    lists[folderIndex].lists.push(list);
                }
            }
            return lists;
        },

        // 將待辦清單陣列依照實際的索引值重新計算其sort屬性，並更新到伺服端
        resortToDoLists() {
            let listIds = [];
            let index = 0;
            for (const listObj of this.toDoLists) {
                if (listObj.type === "folder") {
                    for (const subList of listObj.lists) {
                        subList.Sort = index++;
                        listIds.push(subList.Id);
                        continue;
                    }
                } else {
                    listObj.Sort = index++;
                    listIds.push(listObj.Id);
                }
            }

            axios.post(Router.action("ToDoList", "SortList"), { listIds })
                .then(function (response) {
                    const data = response.data;
                    console.log(data);
                }.bind(this))
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
                }.bind(this));
        },

        /**
         * 將指定陣列元素插入到同一陣列的另一元素之前
         * @param {Array} array  要執行操作的陣列
         * @param {Object} fromElement 要插入的元素
         * @param {Object} insertToElement 要被插入的元素
         */
        insertBefore(array, fromElement, insertToElement) {
            const index = array.findIndex((element) => element === fromElement);
            if (index !== -1)
                array.splice(index, 1);
            const insertIndex = this.toDoLists.findIndex((element) => element === insertToElement);
            array.splice(insertIndex, 0, fromElement);
        },

        /**
         * 將指定陣列元素插入到同一陣列的另一元素之後
         * @param {Array} array  要執行操作的陣列
         * @param {Object} fromElement 要插入的元素
         * @param {Object} insertToElement 要被插入的元素
         */
        insertAfter(array, fromElement, insertToElement) {
            const index = array.findIndex((element) => element === fromElement);
            if (index !== -1)
                array.splice(index, 1);
            const insertIndex = this.toDoLists.findIndex((element) => element === insertToElement);
            array.splice(insertIndex + 1, 0, fromElement);
        },

        // 新增待辦清單
        createList() {
            swal.call(this, {
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
                            if (data.Success === true) {
                                data.Data.sideClass = "";
                                data.Data.type = "list";
                                this.toDoLists.push(data.Data);
                            }
                        }.bind(this))
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
                        }.bind(this));
                },
                allowOutsideClick: () => !swal.isLoading()
            });
        },
    }
});
