function Savedata(fileName, Data) {
  // 保存するJSONファイルの名前
  //const fileName = "mochi.json";

  // データをJSON形式の文字列に変換する。
  const data = JSON.stringify(Data);

  // HTMLのリンク要素を生成する。
  const link = document.createElement("a");

  // リンク先にJSON形式の文字列データを置いておく。
  link.href = "data:text/plain," + encodeURIComponent(data);

  // 保存するJSONファイルの名前をリンクに設定する。
  a.download = fileName;

  // ファイルを保存する。
  a.click();
}

function LoadData() {
  // HTMLのinput要素を生成する。
  const input = document.createElement("input");
  input.type = "file";

  // ファイルが指定された後に行う処理を定義する。
  input.addEventListener("change", e => {
    // ファイルの中身
    var result = e.target.files[0];

    var reader = new FileReader();

    // ファイルの中身をテキストデータとして読み取る。
    reader.readAsText(result);

    // ファイルの中身が読み取られた後に行う処理を定義する。
    reader.addEventListener("load", () => {

      // 拡張子抜きのファイル名を変数に格納する例。
      var title = result.name.match(/(.*)\.json$/)[1];

      // 読み込んだデータ（JSON形式の文字列）をJavaScriptオブジェクトに変換する。
      originalData = JSON.parse(reader.result);
    });
  });

  // 「ファイルを開く」ダイアログを表示する。
  input.click();
}
