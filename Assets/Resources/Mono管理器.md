<b><size=20>Mono 管理器</size></b>

<i>有时候，你不是不想继承 MonoBehaviour，只是不想每个类都继承它。</i>

在 Unity 开发中，我们经常会遇到这样的困境：一个普通的工具类想要使用 Update 功能，但继承 MonoBehaviour 又显得太重了。MonoMgr 就是为了解决这个问题而生的。

<b><size=16>诞生背景</size></b>

想象一下这样的场景：

# 传统做法的问题
public class MyTool
{
    public void DoSomethingEveryFrame()
    {
        // 我希望这个函数每帧都被调用
        // 但我不想让整个类继承 MonoBehaviour
    }
}

传统的做法是创建一个 MonoBehaviour 作为「载体」，然后把 MyTool 挂上去。但这样会让代码结构变得混乱，而且每个需要 Update 的类都要配一个载体，维护起来很麻烦。

MonoMgr 提供了一个<b>集中式的事件订阅机制</b>，让任何类都可以「借用」MonoBehaviour 的生命周期函数。

<b><size=16>核心功能</size></b>

MonoMgr 包装了三个常用的生命周期函数：

<b>事件类型：OnUpdate</b>
对应 Unity 函数：Update
使用场景：每帧更新，如输入检测、动画控制

<b>事件类型：OnFixedUpdate</b>
对应 Unity 函数：FixedUpdate
使用场景：固定时间间隔更新，如物理相关逻辑

<b>事件类型：OnLateUpdate</b>
对应 Unity 函数：LateUpdate
使用场景：在 Update 之后执行，如相机跟随

<b><size=16>使用方法</size></b>

<b>订阅事件</b>

# 订阅 Update 事件
public class MyClass
{
    public MyClass()
    {
        MonoMgr.Instance.AddUpdateListener(MyUpdate);
    }

    void MyUpdate()
    {
        Debug.Log("每帧都会执行");
    }
}

<b>取消订阅</b>

<b>非常重要</b>：当对象被销毁或不再需要更新时，一定要取消订阅，否则会造成内存泄漏。

public void OnDestroy()
{
    MonoMgr.Instance.RemoveUpdateListener(MyUpdate);
}

<b>其他事件</b>

# FixedUpdate
MonoMgr.Instance.AddFixedUpdateListener(MyFixedUpdate);
MonoMgr.Instance.RemoveFixedUpdateListener(MyFixedUpdate);

# LateUpdate
MonoMgr.Instance.AddLateUpdateListener(MyLateUpdate);
MonoMgr.Instance.RemoveLateUpdateListener(MyLateUpdate);

<b><size=16>实际应用示例</size></b>

<b>输入管理器使用 MonoMgr</b>

public class InputMgr : BaseMgr&lt;InputMgr&gt;
{
    private InputMgr()
    {
        // 在构造函数中订阅 Update
        MonoMgr.Instance.AddUpdateListener(Update);
    }

    private void Update()
    {
        // 检测输入...
    }
}

<b>计时器管理器使用 MonoMgr</b>

public class TimerMgr : BaseMgr&lt;TimerMgr&gt;
{
    private TimerMgr() { }

    public void Start()
    {
        // 启动计时器协程
        MonoMgr.Instance.StartCoroutine(TimingCoroutine());
    }

    private IEnumerator TimingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // 每秒执行一次
        }
    }
}

<b><size=16>技术实现</size></b>

MonoMgr 内部使用了 C# 的 <b>event</b> 关键字来实现事件订阅：

private event UnityAction OnUpdate;

public void AddUpdateListener(UnityAction unityAction)
{
    OnUpdate += unityAction;
}

private void Update()
{
    OnUpdate?.Invoke();
}

使用 ?.Invoke() 是为了防止没有订阅者时抛出空引用异常。

<b><size=16>注意事项</size></b>

1. <b>必须取消订阅</b>：忘记 Remove 会导致内存泄漏，被订阅的方法会一直被引用，无法被垃圾回收

2. <b>线程安全</b>：事件回调是在 Unity 主线程中执行的，所以不用担心线程安全问题

3. <b>性能考虑</b>：如果有很多对象订阅了 Update，可能会影响性能。对于大量同类对象的更新，考虑使用对象池统一管理

<b><size=16>与直接继承 MonoBehaviour 的对比</size></b>

<b>特性：代码耦合</b>
MonoMgr 订阅模式：低
直接继承 MonoBehaviour：高

<b>特性：灵活性</b>
MonoMgr 订阅模式：高，可随时订阅/取消
直接继承 MonoBehaviour：低，生命周期由 Unity 控制

<b>特性：适用场景</b>
MonoMgr 订阅模式：工具类、管理器
直接继承 MonoBehaviour：游戏实体、需要可视化的对象

<b>特性：内存开销</b>
MonoMgr 订阅模式：低
直接继承 MonoBehaviour：每个对象都有 MonoBehaviour 开销

<b><size=16>技术地位和意义</size></b>

MonoMgr 是 Yoyo Core 框架中承上启下的一个组件。它让非 Mono 类也能享受到 Unity 生命周期的便利，同时保持了代码的整洁和灵活性。

<i>说到底，这就是一种「委托」的思想——把 Update 的工作委托给专门的管家，自己专注于业务逻辑就好。</i>
