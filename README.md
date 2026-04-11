# YoyoCore
*做游戏嘛，总要有个顺手的框架。*

Yoyo Core 是一个为 Unity 设计的轻量级游戏开发框架。它提供了一套常用的管理器和工具，帮助开发者快速搭建游戏项目的基础架构。(其实是为了方便个人开发独游使用的QAQ)

## 诞生背景

Unity 开发中，我们经常会重复编写一些基础代码：单例管理器、事件系统、对象池、资源加载……这些代码虽然不难，但每次都要重新写一遍，既浪费时间又容易出错。

Yoyo Core 把这些常用的功能封装起来，提供一个开箱即用的解决方案。它不是什么「大一统」的庞大框架，而是像瑞士军刀一样——小巧、实用、顺手。

## 框架特点

| 特点     | 说明                       |
| -------- | -------------------------- |
| 轻量级   | 代码量少，学习成本低       |
| 模块化   | 各组件独立，按需使用       |
| 易扩展   | 基于基类的设计，方便自定义 |
| 中文注释 | 源码全中文注释，阅读无障碍 |

## 框架架构

Yoyo Core 采用**单例管理器 + 工具类**的架构设计：

```
Yoyo Core
├── 管理器层（Managers）
│   ├── 基类：BaseMgr<T> / BaseMgr_Mono<T>
│   ├── MonoMgr - 生命周期管理
│   ├── EventMgr - 事件中心
│   ├── InputMgr - 输入管理
│   ├── PoolMgr - 对象池
│   ├── LoadResourceMgr - 资源加载
│   ├── SceneChangeMgr - 场景切换
│   ├── AudioMgr - 音频管理
│   └── TimerMgr - 计时器管理
├── 工具层（Tools）
│   ├── DebugTool - 调试工具
│   ├── MathTool - 数学工具
│   ├── RayCastTool - UI 射线检测
│   └── CustomTimerTool - 性能计时
└── 配置层（Setting）
    ├── Setting - 全局配置
    └── EventEnums - 事件类型定义
```

## 快速开始

### 1. 导入框架

将`Assets`中的 `CSharp` 和 `Editor` 文件夹复制到你的项目中：

### 2. 使用管理器

```csharp
// 播放背景音乐
AudioMgr.Instance.PlayerBackMusic("MainTheme");

// 订阅事件
EventMgr.Instance.AddEventListener(EventType.GameStart, OnGameStart);

// 从对象池获取对象
GameObject bullet = PoolMgr.Instance.PopObj("Bullet");

// 异步加载场景
SceneChangeMgr.Instance.LoadSceneAsync("Level2", () =>
{
    DebugTool.Log("场景加载完成！");
});
```

## 核心组件详解

### 单例基类

所有管理器都基于单例模式，通过 `Instance` 访问：

```csharp
// 非 Mono 单例
public class MyMgr : BaseMgr<MyMgr>
{
    private MyMgr() { }
    public void DoSomething() { }
}

// 使用
MyMgr.Instance.DoSomething();
```

### 事件系统

模块间通信的核心：

```csharp
// 定义事件类型（EventEnums.cs）
public enum EventType
{
    PlayerHPChange,
    GameOver
}

// 订阅事件
EventMgr.Instance.AddEventListener<int>(EventType.PlayerHPChange, (hp) =>
{
    DebugTool.Log($"血量: {hp}");
});

// 触发事件
EventMgr.Instance.EventTrigger(EventType.PlayerHPChange, 100);
```

### 对象池

高频创建/销毁对象的性能优化方案：

```csharp
// 初始化对象池
PoolMgr.Instance.InitPool(bulletPrefab, 50);

// 获取对象
GameObject bullet = PoolMgr.Instance.PopObj("Bullet");

// 归还对象
PoolMgr.Instance.PushObj(bullet);
```

### 输入管理

将输入与事件系统结合：

```csharp
// 绑定按键
InputMgr.Instance.AddKeyBoardInfo(
    EventType.Jump,
    KeyCode.Space,
    InputInfo.E_InputType.Down,
    () => player.Jump()
);

// 启用/禁用输入
InputMgr.Instance.StartOrCloseInput(false); // 禁用
InputMgr.Instance.StartOrCloseInput(true);  // 启用
```

## 配置说明

通过 `Setting.cs` 可以调整框架行为：

```csharp
public class Setting
{
    public static bool IsOpenLayOut = true;     // 是否开启层级整理
    public static float IntervalTime = 0.05f;   // 计时器更新间隔
    public static bool isDebugMode = true;      // 是否开启调试日志
}
```

**发布前务必将 `isDebugMode` 设为 false！**

## 典型使用场景

### 场景1：玩家射击系统

```csharp
public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Start()
    {
        // 初始化子弹对象池
        PoolMgr.Instance.InitPool(bulletPrefab, 30);

        // 绑定射击按键
        InputMgr.Instance.AddMouseInfo(
            EventType.Fire,
            0,  // 鼠标左键
            InputInfo.E_InputType.Down,
            Fire
        );
    }

    void Fire()
    {
        // 播放音效
        AudioMgr.Instance.PlaySound("GunShot");

        // 从对象池获取子弹
        GameObject bullet = PoolMgr.Instance.PopObj(bulletPrefab.name);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullet>().Launch(transform.forward);
    }
}
```

### 场景2：游戏流程控制

```csharp
public class GameFlow : MonoBehaviour
{
    void Start()
    {
        // 订阅游戏事件
        EventMgr.Instance.AddEventListener(EventType.PlayerDie, OnPlayerDie);
        EventMgr.Instance.AddEventListener(EventType.LevelComplete, OnLevelComplete);

        // 播放背景音乐
        AudioMgr.Instance.PlayerBackMusic("BattleBGM");
    }

    void OnPlayerDie()
    {
        // 延迟2秒后显示游戏结束
        TimerMgr.Instance.CreateTimer(false, 2f, 0, null, () =>
        {
            SceneChangeMgr.Instance.LoadScene("GameOver");
        });
    }

    void OnLevelComplete()
    {
        // 加载下一关
        SceneChangeMgr.Instance.LoadSceneAsync("Level2", () =>
        {
            EventMgr.Instance.EventTrigger(EventType.GameStart);
        });
    }
}
```

### 场景3：UI 管理

```csharp
public class UIMgr : MonoBehaviour
{
    void Start()
    {
        // 订阅属性变化事件
        EventMgr.Instance.AddEventListener<int>(EventType.PlayerHPChange, UpdateHPBar);
        EventMgr.Instance.AddEventListener<int>(EventType.PlayerMPChange, UpdateMPBar);
    }

    void UpdateHPBar(int hp)
    {
        hpBar.value = hp;
    }

    public void OnPauseButtonClick()
    {
        // 暂停游戏
        Time.timeScale = 0;
        InputMgr.Instance.StartOrCloseInput(false);

        // 显示暂停菜单
        pauseMenu.SetActive(true);
    }
}
```

## 最佳实践

### 1. 事件订阅记得取消

```csharp
void OnDestroy()
{
    EventMgr.Instance.RemoveEventListener(EventType.SomeEvent, Handler);
}
```

### 2. 对象池对象状态重置

```csharp
public class Bullet : MonoBehaviour
{
    public void OnSpawn()
    {
        // 重置状态
        lifetime = maxLifetime;
        damage = baseDamage;
    }
}
```

### 3. 使用 DebugTool 而不是直接 Debug.Log

```csharp
// 推荐
DebugTool.Log("信息");

// 不推荐
Debug.Log("信息");
```

### 4. 合理选择管理器基类

```csharp
// 不需要 Update - 使用 BaseMgr
public class DataMgr : BaseMgr<DataMgr> { }

// 需要 Update - 使用 BaseMgr_Mono
public class GameMgr : BaseMgr_Mono<GameMgr> { }
```

### 5. 计时器记得启动

```csharp
// 使用 TimerMgr 前必须先 Start
TimerMgr.Instance.Start();
```

## 扩展建议

Yoyo Core 提供了基础功能，你可以根据项目需求进行扩展：

### 添加新的管理器

```csharp
public class SaveMgr : BaseMgr<SaveMgr>
{
    private SaveMgr() { }

    public void SaveGame()
    {
        // 存档逻辑
    }

    public void LoadGame()
    {
        // 读档逻辑
    }
}
```

### 扩展现有功能

```csharp
// 扩展 EventMgr 支持更多参数
public static class EventMgrExtension
{
    public static void AddEventListener<T1, T2>(this EventMgr mgr, EventType type, UnityAction<T1, T2> action)
    {
        // 实现...
    }
}
```

## 技术总结

Yoyo Core 的设计遵循几个核心原则：

1. **简单优先**：不引入过度复杂的抽象，代码直观易懂
2. **实用主义**：只封装真正常用的功能，避免过度设计
3. **可扩展性**：通过基类和接口提供扩展点
4. **中文友好**：全中文注释，降低国内开发者的学习门槛

它不是完美的，它是一个简单且基础的框架，它会伴随着我的学习而不断完善。

## 相关文档(见Unity编辑器内部)

- [管理器基类](管理器基类.md) - 单例模式的实现
- [Mono 管理器](Mono管理器.md) - 生命周期管理
- [事件中心](事件中心.md) - 模块间通信
- [输入管理器](输入管理器.md) - 输入处理
- [对象池](对象池.md) - 性能优化
- [资源加载](资源加载.md) - 资源管理
- [场景切换](场景切换.md) - 场景管理
- [音频管理器](音频管理器.md) - 音频控制
- [计时器管理器](计时器管理器.md) - 延时任务
- [Debug 工具](Debug工具.md) - 调试输出
- [基本数学工具](基本数学工具.md) - 常用计算
- [射线投射工具](射线投射工具.md) - UI 交互
- [代码耗时检测工具](代码耗时检测工具.md) - 性能分析
- [总设置](总设置.md) - 全局配置
- [事件枚举](事件枚举.md) - 事件类型定义
