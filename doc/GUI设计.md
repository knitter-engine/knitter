# 框架
- 整个GUI系统分3层，ImGui + ComponentGui + ViewGui，其中的ComponentGui和ViewGui依托于ImGui实现。

# ImGui
- ID设计

    `ID的作用是在上一帧和下一帧之间，维持同一个控件的状态、事件。
    为了方便使用，同时又能确保ID的准确性，需要对ID进行仔细设计`

- 底层的ImGui使用图形学接口绘制，基本
- no external string: External strings may cause unknown errors. Unified management of strings can avoid many potential problems
- Interface oriented: The interface makes the responsibilities of each module clear, easy to replace
# ComponentGui
- ComponentGui是基于ImGui的，所有实现仅仅是将ImGui变成有状态的、组件化的。ImGui不支持的功能，ComponentGui必定也无法支持
- 整个Knitter引擎的GameObject，是基于组件模式设计的。因此，UI相关的GameObject，我们也将其设计为组件，所以有了ComponentGui。
# ViewGui
- ViewGui是ComponentGui的上层可视化封装。
- 拥有可视化界面，支持类似WinForm一样，可以通过拖拉拽完成UI的构建。