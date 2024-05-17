# Changelog

## [0.1.10] - 2021-12-16

### fixed

- 打包应用的时候，ConditionalDisplayModifierAttribute.conditionMet和ColorModifierAttribute.backupGUIColor 报警告，变量定义了但未使用

## [0.1.9] - 2021-12-01

### Added

- 为 RectTransform 增加 RayIntersect 拓展函数

## [0.1.8] - 2021-11-29

### Added

- 增加 L10nTextMgr.GetFont 可根据 localization.json id:Font_Home 加载字体

## [0.1.7] - 2021-11-04

### Added

- 增加 Localization Demo 场景
- 为 Localization 模块增加注释
- 为本地化模块增加 Manual 文档

### Changed

- 将原 Document 文件夹重命名为 Documentation
- 根据 Android Locale Config 修改 LangPack 中成员变量的命名

## [0.1.6] - 2021-11-03

### Added

-   增加 Text Localization 模块

## [0.1.5] - 2021-06-15

### Added

-   增加 Overdraw Monitor，可以在 URP 中查看 Overdraw 的情况

### Fixed

-   修复因空文件夹产生的 .meta 文件导致的 Warning 消息

## [0.1.4] - 2021-04-22

### Added

-   增加 Singleton 单例基类
-   增加 ExcludeFromDocs Attribute，用来让特定对象不会产生文档
-   初始化文档文件夹

## [0.1.3] - 2021-03-30

### Fixed

-   修复因为缺少 ChangeLog 对应的 meta 文件导致无法编译的问题

## [0.1.2] - 2021-03-29

### Added

-   增加 Change log
-   增加 `NotifiableList` 容器，该容器可以在元素数量发生变化时，以及元素内容发生改变时，分别通过 `onCollectionChanged` 和 `onElementsPropertyChanged` 接口通知观察者
