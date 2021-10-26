var IndexedDBPlugin = {

    // unityの関数ポインタを格納するオブジェクト
    $funcs: {},

    // IndexDBのパラメータを保持するオブジェクト
    $Params: { db : null, transaction: null},

    // IE9 以降のTextEncoder.encode(str) の代替ポリフィル
    $CustomTextEncoder: function(str) {

        var buf       = new ArrayBuffer(str.length);
        var bufView8  = new Uint8Array(buf);

        for (var i=0, strLen=str.length; i < strLen; i++) {
            bufView8[i] = str.charCodeAt(i);
        }

        return bufView8;
    },

    // トランザクションの生成
    $CreateTransaction: function(StoreName){

        var dbStoreName = Pointer_stringify(StoreName);

        // 登録用トランザクション
        Params.transaction = Params.db.transaction([dbStoreName], "readwrite");

        // すべてのデータがデータベースに追加されたときに行う処理
        Params.transaction.oncomplete = function(event) {
            console.log('commit transaction');
        };

        Params.transaction.onerror = function(event) {
            console.log('rollback transaction');
        };

    },

    // DBを開く
    OpenIndexedDB: function(IndexeDBName, version, successCallback, errorCallback){

        funcs.openSuccessFunc = successCallback;
        funcs.openErrorFunc = errorCallback;

        //既に開いているか
        if(Params.db !== null){
            console.log('db already open');
            // 既に開いているならいったん閉じる
            Params.db.close();
            Params.db = null;
        }

        var dbName = Pointer_stringify(IndexeDBName);

        var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB;

        if (indexedDB) {

            //　DB名とバージョンを指定して接続。DBがなければ新規作成される。
            var openReq  = indexedDB.open(dbName, version);

            openReq.onupgradeneeded = function(event){
                console.log('Upgraded db');
            }

            openReq.onsuccess = function(event){

                // 接続に成功
                console.log('db open success');

                Params.db = event.target.result;

                // Unityに通知
                Runtime.dynCall('v', funcs.openSuccessFunc);

            }

            openReq.onerror = function(event){

                // 接続に失敗
                console.log('db open error');

                Params.db = null;

                // Unityに通知
                Runtime.dynCall('v', funcs.openErrorFunc);

            }

        }else{
            //error
            console.log('db found error');
            // 通知
            Runtime.dynCall('v', funcs.openErrorFunc);
        }
    },

    // DBを閉じる
    CloseIndexedDB: function(){

        if(Params.db !== null){
            Params.db.close();
            Params.db = null;
        }

    },

    // DBを開き、DBに指定のStoreが無ければ作成する。新しいものを作成する場合は以前のものよりversionを高くする
    OpenIndexedDBAndCreateStore: function(IndexeDBName, version, StoreName, successCallback, errorCallback){

        //既に開いているか
        if(Params.db !== null){

            console.log('db already open');
            // 既に開いているならいったん閉じる
            Params.db.close();
            Params.db = null;
        }

        funcs.openSuccessFunc = successCallback;
        funcs.openErrorFunc = errorCallback;

        var dbName = Pointer_stringify(IndexeDBName);
        var dbStoreName = Pointer_stringify(StoreName);

        var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB;

        if (indexedDB) {

            //　DB名とバージョンを指定して接続。DBがなければ新規作成される。
            var openReq  = indexedDB.open(dbName, version);

            openReq.onupgradeneeded = function(event){

                console.log('Upgraded db');

                Params.db = event.target.result;

                // dbStore作成
                var store = Params.db.createObjectStore(dbStoreName, {keyPath: 'myKey'});

                console.log('Upgraded end');

            }

            openReq.onsuccess = function(event){

                console.log('db open success');
                Params.db = event.target.result;
                Runtime.dynCall('v', funcs.openSuccessFunc);

            }

            openReq.onerror = function(event){
                // 接続に失敗
                console.log('db open error');
                // 通知
                Runtime.dynCall('v', funcs.openErrorFunc);
            }

        }else{
            //error
            console.log('db found error');
            // 通知
            Runtime.dynCall('v', funcs.openErrorFunc);
        }
    },

    // DBに値を登録する
    SetIndexedDB: function(StoreName, key, val, successCallback, errorCallback){

        funcs.saveSuccessFunc = successCallback;
        funcs.saveErrorFunc = errorCallback;

        //開いていない場合はエラー
        if(Params.db === null){
            //既に開いているためerror
            console.log('db not open');

            // Unityに通知
            Runtime.dynCall('v', funcs.saveErrorFunc);
            return;
        }

        var dbStoreName = Pointer_stringify(StoreName);
        var saveKey = Pointer_stringify(key);
        var saveValue = Pointer_stringify(val);

        CreateTransaction(StoreName);

        if(Params.transaction != null){

            var store = Params.transaction.objectStore(dbStoreName);

            // キーとバリューを追加(addは追加、putは更新もする)
            var request = store.put({ myKey: saveKey, value: saveValue});

            request.onsuccess = function (event) {

                // Unity側の関数を呼ぶ
                Runtime.dynCall('v', funcs.saveSuccessFunc);

            }

            request.onerror = function(event) {

                // Unityに通知
                Runtime.dynCall('v', funcs.saveErrorFunc);

            }

        }else{
            // Unityに通知
            Runtime.dynCall('v', funcs.saveErrorFunc);
        }
    },

    GetIndexedDB: function(StoreName, key, successCallback, errorCallback){

        funcs.loadSuccessFunc = successCallback;
        funcs.loadErrorFunc = errorCallback;

        //開いていない場合はエラー
        if(Params.db === null){
            //既に開いているためerror
            console.log('db not open');

            // Unityに通知
            Runtime.dynCall('v', funcs.loadErrorFunc);
            return;
        }

        var dbStoreName = Pointer_stringify(StoreName);
        var loadKey = Pointer_stringify(key);

        CreateTransaction(StoreName);

        if(Params.transaction != null){

            var store = Params.transaction.objectStore(dbStoreName);

            var request = store.get(loadKey);

            request.onsuccess = function (event) {

                // 値が存在しない(undefinedがあるため===は使用しない)
                if(this.result == null){
                    //console.log('not data');
                    // Unityに通知
                    Runtime.dynCall('v', funcs.loadErrorFunc);
                    return;
                }

                // valueが存在しない
                if(!this.result.hasOwnProperty('value')){
                    //console.log('not value');
                    // Unityに通知
                    Runtime.dynCall('v', funcs.loadErrorFunc);
                    return;
                }

                var resultVal = this.result.value;

                // TextEncoder.encode(str)は互換が怪しいためポリフィルを使用。文字列はnull文字終端にする
                var strBuffer = CustomTextEncoder(resultVal + String.fromCharCode(0));

                var strPtr = _malloc(strBuffer.length);
                HEAP8.set(strBuffer, strPtr);

                // Unity側の関数を呼ぶ
                Runtime.dynCall('vi', funcs.loadSuccessFunc, [strPtr]);

                _free(strPtr);
            }

            request.onerror = function(event) {

                // Unityに通知
                Runtime.dynCall('v', funcs.loadErrorFunc);

            }

        }
    },

};

autoAddDeps(IndexedDBPlugin, '$funcs');
autoAddDeps(IndexedDBPlugin, '$Params');
autoAddDeps(IndexedDBPlugin, '$CustomTextEncoder');
autoAddDeps(IndexedDBPlugin, '$CreateTransaction');
mergeInto(LibraryManager.library, IndexedDBPlugin);