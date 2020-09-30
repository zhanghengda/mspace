# 接口说明
## 1、POI标注接口
### 1.1 javaScript调用c#

#### 打开台账
```javascript
//1为点，2为线，3为面
window.external.openAccountView(poiId,geotype)
```

#### 创建标注
```javascript
//1为点，2为线，3为面
window.external.createMarkerPoi(geotype)
```

#### 更新标注
```javascript
//1为点，2为线，3为面
window.external.updateMarkerPoi(poiId,geotype)
```

#### 删除标注
```javascript
window.external.deleteMarkerPoi(poiId)
```

#### 定位标注
```javascript
window.external.flyToMarkerPoi(poiId)
```

#### 设置标注可见
```javascript
window.external.setMarkerPoiVisible(poiInfo,isVisilbe)
```
#### 新增台账
```javascript
window.external.AddAccount(markerId)
```

#### 设置标注组可见
```javascript
//poiIds的集合字符串，以,为分隔符，如0001,0002
//isVisilbe 标注是否可见
//页序号
window.external.setMarkerPoiVisible(poiInfos,isVisilbe,pageIndex)
```

### 1.2 c#调用javaScript

#### 创建,更新,删除,标注成功后调用
```javascript
webView.InvokeScript("updateMarkers");
```


#### 开启多屏对比
```javascript
webView.InvokeScript("openCompareView",isChecked)
```

## 2、图层操作

###增加图层数据源
```javascript
/// <summary>
/// 增加数据源
/// </summary>
/// <param name="SourceType">数据类型</param>
window.external.AddDataSource(SourceType)
```javascript


###删除图层数据源
```javascript
/// <summary>
/// 删除数据源
/// </summary>
/// <param name="LayerGuid">图层id</param>
window.external.DeleteDataSource(LayerGuid)
```javascript

### 图层组是否可见
```javascript
/// <summary>
/// 设置图层组是否可见
/// </summary>
/// <param name="LayerGuids">图层组下的layer guid的集合字符串，以,为分隔符，如0001,0002</param>
/// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
/// <param name="isVisilbe">图层是否可见</param>
window.external.setRederLayersVisible(LayerGuids,viewPortIndex,isVisilbe)
```

### 图层是否可见
```javascript
/// <summary>
/// 设置图层是否可见
/// </summary>
/// <param name="LayerGuid">图层guid</param>
/// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
/// <param name="isVisilbe"></param>
window.external.setRederLayerVisible(LayerGuid,viewPortIndex,isVisilbe)
```

### 设置分屏状态
```javascript
/// <summary>
/// 设置分屏状态
/// </summary>
/// <param name="compareViewState">分屏状态，2表示2屏，以此类推</param>
window.external.setCompareViewState(compareViewState)
```

### 飞入图层
```javascript
/// <summary>
/// 飞入图层
/// </summary>
/// <param name="LayerGuid">图层id</param>
window.external.flyToRederLayer(LayerGuid)
```

## 3、登录操作 

```javascript
/// <summary>
/// 登录
/// </summary>
/// <param name="isLogin">是否登录成功</param>
/// <param name="token">用户token</param>
window.external.isLogin(isLogin,token)
```
