# Changelog

## [0.4.21] - 2021-12-16

### Changed

- 将 Utilities 的依赖版本提升至 0.1.10
- 手柄材质球丢失shader引用


## [0.4.20] - 2021-12-16

### Fixed

- 修复 C++ 侧 RenderLayer 初始化时可能存在的 ColorHandle 脏内存
- 修改SDK的说明文档和接口注释

## [0.4.19] - 2021-12-13

### Fixed

- 修改Boundary接口GetGemetry数据和安全边界显示的不一致

### Added

- 增加 UnderlayPuncher_Transparent_KeepColor shader，该 Shader 可在打洞的基础上保留有色彩缓冲原先的颜色

## [0.4.18] - 2021-12-07

### Fixed

- 修改2DLauncher在绘制GL_TEXTURE_EXTERNAL_OES类型纹理时概率性crash

## [0.4.17] - 2021-12-06

### fixed

- 修复YVRGetBoundaryConfigured一直返回true
- OnlyVR 第一次导入的时候默认设置为Ture没有生效

### Added

- 增加接口YVRGetBoundaryGeometryPointsCount，获取安全边界顶点数量

## [0.4.16] - 2021-12-02

### Added

- 增加 onRecenterOccurred 事件，表示在当前帧发生了重定位

### Changed

- 使用 GetClickedController / GetTouchedController 取代 GetUsedController

### Fixed

- 修复 YVRCameraRig 上的丢失脚本

## [0.4.15] - 2021-12-01

### Added

- YVRInputModule 中添加 GetFirstRaycast / inputDataProviderSource 数据

### Changed

- 适配 RectTransform 的拓展函数 RayIntersects
- 将 Utilities 的依赖版本提升至 0.1.19

## [0.4.14] - 2021-11-30

### Changed

- 当左右手柄都连接时，默认使用右手柄作为 Raycaster 的数据提供者

### Fixed

- 修复多 Canvas 时交互错误的问题
- 修复应用切换后，屏幕刷新率改动失效的问题

## [0.4.13] - 2021-11-29

### Added

- 增加 usedController 接口表示最新使用的手柄
- 在 YVRInputModule 中增加对 Drop Event 的支持

### Changed

- yvrDataControllerProvider 中默认以 Controller Anchor 作为射线七点

### Fixed

- 修复当一个 Graphic Raycaster 无 Event Camera 时导致所有 Graphic Raycasters 失效的问题。

## [0.4.12] - 2021-11-23

### Added

- YVRInputModule 添加customProvider 支持自定义输入数据。

## [0.4.11] - 2021-11-18

### Changed

- Internal YVRSetBoundaryTypeVisible 接口增加关于是否是退出的 bool 值判断。
- 为 CompositeLayer 示例场景增加 Discard / Transparent Underlay Puncher 示例场景。

### Fixed

- 修复在 Unity EyeBuffer 创建前，修改它们 FFR 系数导致异常的问题
- 非 Raycast Target 的 UI 元素仍然会被交互

## [0.4.10] - 2021-11-17

### Added

- 增加初始化CompositeLayer时，指定CompositeDepth的接口。

## [0.4.9] - 2021-11-09

### Added

- 增加内部 API 接口 YVRSetBoundaryTypeVisible(yvrlib_internal_SetThisBoundaryTypeVisible)

## [0.4.8] - 2021-11-04

### Added

- 在打包的时候自动在androidmanifest文件中添加 vr_only

### Changed

- 更新 API_LIB release 仓库

### Fixed

- 修复非 Dynamic Composite Layer 无法渲染的问题

### Removed

- 删除 Tracking Mode 相关接口

## [0.4.7] - 2021-11-01

### Added

- 增加 URP 手材质为半透明描边

### Changed

- 优化手部动画状态机

### Fixed

- 修复头盔速度，加速度无法获取的问题
- 修复使用 Dynamic Composite Layer 时，画面撕裂问题

## [0.4.6] - 2021-11-01

### Added

- Composite Layer 支持多块 Swapchain Buffer，用以解决画面撕裂问题

### Changed

- 减少 Unity Eye Buffer 内存消耗，之前 SwapChain 的 Buffer 数为4，先改为最低满足要求的 Triple Buffer。

## [0.4.5] - 2021-10-30

### Added

- 增加编译内部文档的支持，当执行命令 BuildDocument.bat 3 时即为编译内部文档。
- 支持动态切换 Extra Latency 模式

### Changed

- 将 手势相关的 Mesh 数据移动到 Mesh 文件夹中
- 将文件夹命名从 document 调整为 documentation
- 使用 C# ??= 关键字简化初始化流程

### Fixed

- 修复文档中关于 Package 的错误说明
- 修复当 ControllerType 为 None时，返回值错误的问题。之前当为 None 时，返回的为 Int.MinValue 或 Float.MinValue，现在统一调整为 0。
- 修复当 Target ControllerType 是 None 或 All 时 IsControllerConnect 返回值错误的问题。
- 修复手柄断开连接后，按键数据错误的问题。

### Removed

- 删除 GetAppFrameRate 接口，后续开发者可以通过 System UI 查看帧率
- 删除无用 Dominant Hand 接口，目前 Launcher 内并无设计相关 UI

## [0.4.4] - 2021-10-23

### Added

- 暴露颞部接口 OnGuardianEnter

### Changed

- 更新 Submodule URL

## [0.4.3] - 2021-10-22

### Added

- 增加内部 API 接口 YVRGetBoundaryGameTypeDataStatus
- 支持直接将 Unity 的 RT 直接绑定到 SwapChain 上。

### Removed

- 修复 Boundary 相关 API 不生效的问题

## [0.4.2] - 2021-10-22

### Fixed

- 修复SetLayerFlags 失效的问题，且重命名为 LockLayerFlags

## [0.4.1] - 2021-10-21

### Added

- 支持运行时动态切换 Composite Layer 显示状态
- 增加 CompositeLayer Demo 场景

### Changed

- 工程包名修改为全小写
- 删除部分无用资源，将 Demo 场景相关资源移动到 Scene 中
- 为 YVRHandAnimData 脚本添加命名空间

### Removed

- 删除 IsRecenterOccurred 查询时的 Log 输出

## [0.4.0] - 2021-10-14

### Added

- 支持 Quad Composite Layer，包括 Overlay 及 Underlay
- 增加 手+手柄 模型的  URP 材质

### Changed

- Native 重构渲染模块，进一步封装各渲染操作


## [0.3.27] - 2021-10-08

### Added

- 在 EnterVR 时默认开启 ExtraLatencyMode

## [0.3.26] - 2021-10-06

### Changed

- 更新 API_LIB 版本至 1a3b1d655a4c04af

## [0.3.25] - 2021-09-30

### Changed

- 更新 API_LIB 版本至 4c9789b70d91eb8d3b918de7b78d50e3761ff122

## [0.3.24] - 2021-09-29

### Changed

- 更新 API-LIB 版本至 0.3.24
- 手柄模型设置为 IsReadAble

## [0.3.23] - 2021-09-28

### Added

- 新增 SetLayerMatrix 内部接口

### Changed

- 更新 API_LIB 至 1.0.1.1

## [0.3.22] - 2021-09-26

### Changed

- 更新 API_LIB 至版本 11a3fb697d216fcba628e3ba4adfd595d2b5c9

## [0.3.21] - 2021-09-25

### Changed

- 调整手柄手势贴图文件大小
- Native 不再进行手柄坐标系转换，默认数据为世界坐标


## [0.3.20] - 2021-09-23

### Added

- 新增内部 Get/Set BoundaryType 接口
- 增加手柄手势相关模型及动画

### Fixed

- 修复 GetPositionTracked 数据错误问题

## [0.3.19] - 2021-09-16

### Changed

-   更新 API_LIB, 主要修复手柄 Track 状态的获取问题

## [0.3.18] - 2021-09-16

### Changed

-   更新 API_LIB, 主要提升部分接口性能问题

## [0.3.17] - 2021-09-13

### Changed

-   SetTrackingMode, Recenter 调整为 Internal 接口
-   isUserPresent 调整为 bool 值范围

### Removed

-   C# 端删除 SetTrackingMode，Recenter 接口暴露

## [0.3.16] - 2021-09-13

### Changed

-   减少 Input 和 ControllerRig 更新函数造成的内存分配

### Fixed

-   修复无速度，加速度等数据的问题
-   修复 isUserPresent 返回值错误问题

### Removed

-   删除 resetOnRelaoded 接口
-   暂时删除 UserPresent 检测

## [0.3.15] - 2021-09-08

### Changed

-   使用 4 float 表示 Render Layer 的顶点

## [0.3.14] - 2021-09-06

### Added

-   增加 YVREventsMgr 管理 YVR 相关事件，目前仅内部暴露 Recenter Occurred 事件。

### Fixed

-   修复左手柄无法触发 All 按键的问题。
-   修复 None 按键会被触发的问题

### Removed

-   目前手柄侧键是按键而非是 Trigger，因此从 API 中删除相关数据。

## [0.3.13] - 2021-08-28

### Added

-   增加内部接口 YVRClearBoundaryData 清除所有 Boundary 数据

## [0.3.12] - 2021-08-28

### Added

-   增加 YVRForceBoundaryNoneVisible API 来强制管理安全边界（无视当前是否碰撞到安全边界）

## [0.3.11] - 2021-08-26

### Fixed

-   修复 IPD Tracking 失效的问题

## [0.3.10] - 2021-08-25

### Added

-   初步实现 Overlay 功能，已知问题：
    1. Overlay 位置与 EyeBuffer 不完美匹配
    2. Overlay 在位置变换时 UV 计算错误
    3. 不支持动态修改 Overlay 内容

### Fixed

-   修复 Tracking Space 失效问题

## [0.3.9] - 2021-08-21

### Fixed

-   修复 YVRSetLayerFlags API 失效的问题

## [0.3.8] - 2021-08-19

### Changed

-   重构 Unity Native 代码结构，拆分功能至对应模块，如 Rendering，Tracking，UnityXR
-   增加 YVRRenderLayersMgr 管理 Frame Layers
-   适配 API LIB 关于 TextureLayer 的修改，目前以 4 个顶点修饰对应的 Layer

### Fixed

-   修复无启动 Splash 时 SDK Crash 的问题

## [0.3.7] - 2021-08-16

### Changed

-   更新 APILIB 至版本 237c98a0

## [0.3.6] - 2021-08-11

### Added

-   增加 GetPositionTracked / GetOrientationTracked 接口判断手柄追踪状态

### Fixed

-   修复 MultiView 依赖过时 XR Settings 的问题

## [0.3.5] - 2021-08-09

### Added

-   增加 ControllerTracked 接口判断手柄是否正在被追踪

### Fixed

-   修复程序从休眠唤醒后重复添加 Render Layers 的问题

## [0.3.4] - 2021-08-06

### Added

-   增加 SetBoundaryVisible API_LIB
-   Project 增加 vr-only 标记

### Changed

-   增加 YVRAPILib_release 作为提供 AAR 编译需要的 so 和头文件的 Submodule，并适配对应编译方式

### Fixed

-   修改由 Native 坐标系修改导致的手柄坐标错误问题
-   修复由 System.Net 依赖导致的编译错误问题
-   修复由 API Lib 导致的预测时间错误问题

## [0.3.3] - 2021-08-04

### Added

-   支持 Gamma /Linear 切换
-   支持 FFR 渲染
-   支持在 YVR XR Settings 中设置 Tracking Mode。运行时的 Tracking mode 设置仍通过 TrackingStateManager 修改
-   支持 Tracking Space 相关设置
-   当开启 RecommendedMSAALevel 时，自动设置 MSAA Level
-   在 InputDataProvider 中添加属性 用来读取当前控制器摇杆的上下拨动

### Changed

-   适配 API_LIB 中所有接口的重命名
-   适配 Server 中头盔坐标系的调整

### Fixed

-   修复开启 Single Pass 时仅左眼部分有画面问题

### Removed

-   删除 C# 端无用设置

## [0.3.2] - 2021-07-30

### Added

-   增加 Render Scale 支持
-   增加 16-bit depth Buffer 支持
-   增加选择是否要使用 IPD 区分双眼的支持

### Fixed

-   修复 Layer / Frame 相关设置不生效的问题
-   修复 PC Emulator 不生效的问题

## [0.3.1] - 2021-07-28

### Added

-   增加 YVRGetRecenterPose 接口
-   支持 Multi Passes 和 Single Pass 的切换，但开启 Single Pass 时仍然需要依赖 Deprecated XR Settings

### Changed

-   替换左手柄模型，更加符合更新后的实际手柄

## [0.3.0] - 2021-07-26

### Added

-   BREAKING! 渲染和追踪模块接入 Unity XR Subsystem

### KNOWN ISSUE

-   FFR / Render Scale/ Overlay 暂不支持
-   目前强制开启 Single Pass，暂不支持切换

## [0.2.7] - 2021-07-20

### Added

-   CreateSwapChain 时增加对 ArraySize 的支持

### Changed

-   更新手柄模型

## [0.2.6] - 2021-07-19

### Added

-   增加 SetPassThrough 的 Internal 接口

### Fixed

-   修复 Layer Flag 不生效的问题

## [0.2.5] - 2021-07-15

### Added

-   暴露部分接口给 Guardian 和 Controller Binding 使用

## [0.2.4] - 2021-07-12

### Added

-   提供一个可以设置 InputDataProvider 的方法

## [0.2.3] - 2021-07-10

### Added

-   Add item

### Changed

-   在 BeginVR 时默认设置 CPU/GPU Level

### Fixed

-   当使用非对称 FOV 时，渲染错误的问题

## [0.2.2] - 2021-07-09

### Changed

-   更新版本 API LIB 版本至 15e7e3188a6cbfbdb8c494d5e390418947e96a35
-   将 EnterVR 调整为同步 API

### Fixed

-   修复在进入 VR 模式前，会渲染几帧普通 2D 画面的问题

## [0.2.1] - 2021-07-07

### Changed

-   更新 API_LIB AAR 版本，修改 internal 函数的接口命名
-   增加 CHANGELOG_User_Eng，为供用户查看的文档

## [0.2.0] - 2021-07-07

### Added

-   文档中增加 Global Search 功能

### Changed

-   BREAKING! 修改 Native 接口为 VR Runtime 版本

### Fixed

-   修复手柄 Index 定义错误导致手柄不震动的问题

## [0.1.15] - 2021-06-24

### Changed

-   更新 Native AAR 至 b5b7fbffd26 版本

## [0.1.14] - 2021-06-19

### Added

-   增加 SetBoundaryData 和 GetRawHeadPose 接口

### Changed

-   调整部分代码访问修饰符，满足自动化测试要求

## [0.1.13] - 2021-06-18

### Added

-   新增 FrameOption 接口，可用来配置渲染帧状态，如是否需要 ATW

### Changed

-   更新 YVR Utilities 依赖至 0.1.5 版本
-   无视 Rider 编辑器自动生成的文件夹

## [0.1.12] - 2021-06-07

### Added

-   YVRRenderLayer 增加修改 Composition Depth 接口
-   新增 SetBeginVROption 和 UnsetBeginVROption 接口，可用来修改 BeginVR 启动参数

### Changed

-   修改 BeginVR 参数传递方式

## [0.1.11] - 2021-06-07

### Added

-   Native 新增手柄配对接口

## [0.1.10] - 2021-06-04

### Changed

-   更新 YVR API Lib

## [0.1.9] - 2021-05-27

### Changed

-   替换 Oculus 手柄模型为 YVR 手柄

## [0.1.8] - 2021-05-26

### Added

-   Input Scene 增加更多调试信息
-   支持非对称 FOV 渲染
-   修复 Cursor 法线方向为 zero 时产生 Warning 信息

### Fixed

-   修复 Input Scene 场景中文字过长时，Debug Panel 空白问题

## [0.1.7] - 2021-05-13

### Changed

-   更新 YVR_API_LIB

### Fixed

-   修复摇杆数据没法返回负值的问题

## [0.1.6] - 2021-05-11

### Added

-   增加 enableHMDRayAgent Flag。当该 Flag 开启且双手柄都未连接时，会自动切换头盔作为交互射线的 Anchor，且 默认每 2 秒自动实现一次点击。

### Fixed

-   修复当 CameraRig 的旋转角度不为 0 时，模拟的手柄位置错误的问题
-   修复在左右手柄连接且未指定目标手柄时，所有的获取手柄状态的接口，都返回左手边数据。

## [0.1.5] - 2021-04-30

### Added

-   增加配置文件对 gUsingCustomDistort 的解析，用来切换畸变算法
-   增加 YVR Controller Emulator 中 TargetController，用来切换模拟的手柄
-   增加头盔/手柄模拟器相关文档
-   增加 Fixed Foveated Rendering 文档
-   增加 Single Pass 文档

### Changed

-   优化 Update/LateUpdate 时的内存分配
-   文档中 Debug Scenes 改名为 Demo Scenes

### Fixed

-   修复重定位后，手柄姿态错误问题

## [0.1.4] - 2021-04-26

### Added

-   增加对于 Unity Utilities 的文档引用
-   增加文档对于 PlantUML 的支持
-   增加 ExcludeFromDocsAttribute Attribute，可用于让特定内容不被文档引用
-   增加关于 VYRManager 的说明文档
-   增加 Single Pass 功能
-   增加 GL 宏，简化 Native 中 Check OpenGL Error 的流程
-   增加手柄位置调试接口

### Changed

-   YVR Utilities 的依赖版本为 0.1.4
-   调整工程目录

### Fixed

-   修复文档无法编译的问题
-   修复手柄摇杆和扳机数据错误的问题
-   修复渲染相关的内容设置，在 Inspector 窗口中不显示的问题

## [0.1.3] - 2021-03-31

### Fixed

-   修复手柄按键无效的问题

## [0.1.2] - 2021-03-31

### Added

-   增加获取/设置 CPU / GPU 等级的接口
-   增加获取 CPU / GPU 利用率的接口
-   增加 Change Log 管理每一次更新内容

### Changed

-   使用 Package 方式管理依赖的 Utilities 包，依赖版本为 0.1.3

### Fixed

-   修复手柄纹理错误问题
