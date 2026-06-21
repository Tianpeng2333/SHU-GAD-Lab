# Unity-Wwise Audio Design Assignment Breakdown

**使用 Unity 引擎和 Wwise 音频中间件，完成下述音频设计的各个模块内容：**

- 空间音频 Spatial Audio with Room and Portal
- 基础环境声 Ambience
- 物件 3D Object
- 角色脚步 Character Foley Footstep

## 项目要求

### 一、开发环境

- 完成 Unity 和 Wwise 的安装和整合，跑通资产创建、打包和调用的过程
- 创建 GitHub 账户和 Repo 仓库，开发过程中确保有正常的上传记录

### 二、Wwise 音频中间件

- 创建一个环境声基底资产，包括可循环播放设置，以及相应的 3D 化 Positioning 设置和 Attenuation 衰减曲线
- 创建一个篝火物件的声音资产，包括 Random 和 Blender Container 层级结构的具体设置，以及相应的 3D 化 Positioning 设置和 Attenuation 衰减曲线
- 创建一个角色跑步的声音资产，包括 Game Sync Switch 两种鞋子类型和四种材质类型和相应的 Switch Container 嵌套层级结构的具体设置
- 创建一条用于模拟空间混响的 Aux Bus，包括 Effect 插件和相应的 3D 化 Positioning 设置，以及声音传送到 Aux Bus 的设置
- 所有资产的结构和命名合理，并创建相应的 Event 资产用于引擎中调用

### 三、Unity 引擎

- 在地图场景中使用 Room 和 Portal 对象创建室内外空间和联通，以及组件内各个 Component 相对应功能的具体设置
- 室外空间需要播放环境声基底，以及实现室内外空间联通处的衰减过渡效果
- 室内空间需要设置混响效果
- 使用蓝图对象的方式创建一个篝火物件，摆放至地图中的室内空间，正确播放且带有混响效果
- 角色跑步声资产以 Notify 的形式配置在角色动画中，并指定到对应的 Socket 上
- 创建四种 Material 材质 Dirt、Rock、Wood 和 Grass，以及对应的 Physical Material 和 Surface Type，并将材质正确配置到场景中
- 对 Notify 的功能进行改造，实现 Raycast 射线检测材质的功能

### 四、整体表现

- 地图场景可自行设计，能够体现上述音频设计内容即可
- 确保 Play in Editor 能够正确运行游戏
- 调整上述各种声音的响度比例，保证听感舒适，参考响度范围 -27 至 -40 LUFS 之间

---