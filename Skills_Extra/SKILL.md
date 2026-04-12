
* name：yoyo-core-skill
* description：Use Yoyo_Core with Skill

当用户要求"使用核心"、"使用Core"或"使用yoyo"或"-Core"时，可以应用此技能。

## 书写工作流

1. 从用户输入或大纲中确定要调用的规范

2. 根据Yoyo_Core中的相关代码进行拓展

3. 整体检查代码是否出现报错

## Yoyo_Core使用要点

* 如果代码与Yoyo_Core规范冲突，优先使用Yoyo_Core机制
* 不改变Yoyo_Core中的任何代码（除了EventEnums中的事件枚举）
* 不在Core文件夹下创建新文件
* 需要了解具体模块时，打开references文件夹下的对应文档查询：
  - [管理器基类](references/管理器基类.md) - BaseMgr<T>和BaseMgr_Mono<T>的使用规范
  - [事件中心](references/事件中心.md) - EventMgr的事件订阅和触发机制
  - [事件枚举](references/事件枚举.md) - EventType定义规范和命名约定
  - [对象池](references/对象池.md) - PoolMgr的对象复用管理
  - [Mono管理器](references/Mono管理器.md) - MonoMgr的生命周期订阅机制
  - [计时器管理器](references/计时器管理器.md) - TimerMgr的延时任务管理
  - [资源加载](references/资源加载.md) - LoadResourceMgr的资源加载规范
  - [音频管理器](references/音频管理器.md) - AudioMgr的音频播放管理
  - [输入管理器](references/输入管理器.md) - InputMgr的输入事件绑定
  - [基本数学工具](references/基本数学工具.md) - MathTool的常用数学计算
  - [Debug工具](references/Debug工具.md) - DebugTool的日志输出控制
  - [场景切换](references/场景切换.md) - SceneChangeMgr的场景加载规范
  - [代码耗时检测工具](references/代码耗时检测工具.md) - CustomTimerTool的性能测量
  - [射线投射工具](references/射线投射工具.md) - RayCastTool的UI射线检测
  - [总设置](references/总设置.md) - Setting类的全局配置说明
