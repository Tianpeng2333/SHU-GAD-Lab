# Unity-Wwise 作业实施清单

> 编码：UTF-8。本文按 `Unity_Wwise_Audio_Design_Assignment_Breakdown.md` 的要求拆成可执行步骤。

## 0. 当前项目状态

- 当前目录只有作业文档和 `Sound/` 音频素材，还不是 Git 仓库。
- Unity 项目名：`SHU_GAD_WwiseLab`。
- 主场景：`SHU_GAD_WwiseLab/Assets/Scenes/Main.unity`。
- 需要通过 Wwise Launcher 集成 Wwise。
- 所有文本文件建议保存为 UTF-8。

## 1. 推荐命名

### Wwise Event

- `Play_AMB_Forest_Outdoor`
- `Stop_AMB_Forest_Outdoor`
- `Play_OBJ_Campfire`
- `Stop_OBJ_Campfire`
- `Play_FOL_Footstep_Run`

### Wwise Game Sync

- Switch Group: `ShoeType`
  - `Highheel`
  - `Debris`
- Switch Group: `SurfaceType`
  - `Dirt`
  - `Rock`
  - `Wood`
  - `Grass`

### Unity Tag/Layer/Surface

- Physic Material: `PM_Dirt`, `PM_Rock`, `PM_Wood`, `PM_Grass`
- Material: `MAT_Dirt`, `MAT_Rock`, `MAT_Wood`, `MAT_Grass`
- Surface Enum/String: `Dirt`, `Rock`, `Wood`, `Grass`

## 2. 素材对应关系

### Ambience

- 基底循环：
  - `Sound/Ambience/AMB_Forest/AMB_Forest_2D_Insect_Group_Night.wav`
  - `Sound/Ambience/AMB_Forest/AMB_Forest_2D_Wind_Gentle.wav`
- 3D 随机点声源，可选：
  - `AMB_Forest_3D_Bird_Owl_Hoot_*.wav`
  - `AMB_Forest_3D_Insect_Cricket_*.wav`
  - `AMB_Forest_3D_Insect_Grasshopper_*.wav`
  - `AMB_Forest_3D_Tree_Crack_*.wav`
  - `AMB_Forest_3D_Tree_Rustle_*.wav`
  - `AMB_Forest_3D_Water_Stream.wav`
  - `AMB_Forest_3D_Wolf_Howl_*.wav`

### Campfire Object

- Loop：
  - `Sound/Object/OBJ_Campfire_Burn_Loop.wav`
- Random Crack：
  - `Sound/Object/OBJ_Campfire_Crack_01.wav` 至 `OBJ_Campfire_Crack_07.wav`
- Random Sizzle：
  - `Sound/Object/OBJ_Campfire_Sizzle_01.wav` 至 `OBJ_Campfire_Sizzle_07.wav`

### Footstep

- Highheel 四种地表：
  - `Sound/Foley/Foley_Footstep/FOL_FS_Highheel/FS_Highheel_Dirt/`
  - `Sound/Foley/Foley_Footstep/FOL_FS_Highheel/FS_Highheel_Rock/`
  - `Sound/Foley/Foley_Footstep/FOL_FS_Highheel/FS_Highheel_Wood/`
  - `Sound/Foley/Foley_Footstep/FOL_FS_Highheel/FS_Highheel_Grass/`
- Debris 四种地表：
  - `Sound/Foley/Foley_Footstep/FOL_FS_Debris/` 中的 `Dirt/Rock/Wood/Grass` 文件

## 3. Wwise 设置步骤

### 3.1 Ambience

1. 在 Actor-Mixer Hierarchy 新建 `AMB_Forest_Outdoor`。
2. 新建 Blend Container 或 Actor-Mixer，放入两个 2D 循环文件：`Insect_Group_Night` 和 `Wind_Gentle`。
3. 两个 Sound SFX 都开启 Loop。
4. Positioning：
   - 基底层可使用 2D。
   - 若加入鸟叫、虫鸣、树枝、水流等点声源，设置为 3D Spatialization。
5. Attenuation 建议：
   - Max distance: 20-35 m。
   - 曲线从 0 m 的 0 dB 平滑衰减到最大距离的 -60 dB。
6. 创建 Event：`Play_AMB_Forest_Outdoor` 和 `Stop_AMB_Forest_Outdoor`。

### 3.2 Campfire

1. 新建 Blend Container：`OBJ_Campfire`。
2. 子层级：
   - Sound SFX：`OBJ_Campfire_Burn_Loop`，开启 Loop。
   - Random Container：`OBJ_Campfire_Crack_Random`，放入 7 个 Crack。
   - Random Container：`OBJ_Campfire_Sizzle_Random`，放入 7 个 Sizzle。
3. Crack/Sizzle Random Container：
   - Random mode: Shuffle 或 Random。
   - Avoid repeating last: 1。
   - 可用 Trigger rate 或容器内延迟制造不规则触发。
4. Positioning：
   - `OBJ_Campfire` 设置 3D。
   - Attenuation Max distance 建议 10-15 m。
5. Output Bus 发送到空间混响 Aux Bus。
6. 创建 Event：`Play_OBJ_Campfire` 和 `Stop_OBJ_Campfire`。

### 3.3 Footstep Switch Container

1. Project Explorer > Game Syncs 新建 Switch Group：`ShoeType`。
2. 添加 Switch：`Highheel`、`Debris`。
3. 新建 Switch Group：`SurfaceType`。
4. 添加 Switch：`Dirt`、`Rock`、`Wood`、`Grass`。
5. Actor-Mixer Hierarchy 新建 Switch Container：`FOL_Footstep_Run`。
6. 第一层按 `ShoeType` 分支：
   - `Shoe_Highheel`
   - `Shoe_Debris`
7. 每个鞋子分支内再嵌套一个按 `SurfaceType` 的 Switch Container。
8. 每个地表分支放一个 Random Container：
   - `Run_Highheel_Dirt_Random`
   - `Run_Highheel_Rock_Random`
   - `Run_Highheel_Wood_Random`
   - `Run_Highheel_Grass_Random`
   - `Run_Debris_Dirt_Random`
   - `Run_Debris_Rock_Random`
   - `Run_Debris_Wood_Random`
   - `Run_Debris_Grass_Random`
9. 优先导入文件名里包含 `Run` 的脚步；如果 Debris 没有 Run 分类，就用 `FOL_FS_Debris_*` 作为碎屑鞋底的通用跑步层。
10. 创建 Event：`Play_FOL_Footstep_Run`。

### 3.4 Reverb Aux Bus

1. Master-Mixer Hierarchy 新建 Aux Bus：`Aux_Room_Reverb_Small`.
2. 插入 Reverb 插件，例如 `Wwise RoomVerb`。
3. 建议初始值：
   - Reverb Time: 1.2-1.8 s。
   - Wet Level: -12 dB 至 -8 dB。
   - Dry Level: 0 dB。
4. 需要混响的对象通过 Game-defined Auxiliary Sends 或 Room/Aux Send 送入该 Aux Bus。

### 3.5 SoundBank

1. 创建 SoundBank：`Main`.
2. 加入所有 Events。
3. Generate SoundBanks。
4. 确认 Unity 里能看到并加载 SoundBank。

## 4. Unity 设置步骤

### 4.1 工程和集成

1. 打开 Unity 项目：`SHU_GAD_WwiseLab`。
2. 用 Wwise Launcher 对该 Unity 项目执行 Integrate Wwise into Project。
3. 打开 Unity 后检查菜单里是否出现 Wwise。
4. 将 Wwise 生成的 SoundBank 路径同步到 Unity 项目。

### 4.2 场景

1. 创建一个室外区域：森林空地。
2. 创建一个室内区域：小屋或石室。
3. 室内外之间留一个门洞作为 Portal。
4. 放置玩家角色和摄像机。

### 4.3 Room 和 Portal

1. 室内区域创建 GameObject：`Room_Indoor`。
2. 添加 `AkRoom`，Room Tone 或 Aux 设置指向 `Aux_Room_Reverb_Small`。
3. 室外区域创建 GameObject：`Room_Outdoor`。
4. 添加 `AkRoom`，用于空间连接和环境声过渡。
5. 门洞位置创建 GameObject：`Portal_Door`。
6. 添加 `AkRoomPortal`，连接 `Room_Indoor` 与 `Room_Outdoor`。
7. 调整 Portal 尺寸覆盖门洞。

### 4.4 Ambience

1. 在室外区域创建 `AMB_Forest_Outdoor_Emitter`。
2. 添加 `AkGameObj` 和 `AkEvent`。
3. Event 选择 `Play_AMB_Forest_Outdoor`。
4. Trigger 选择 `Start` 或场景开始时手动 Post。

### 4.5 Campfire Prefab

1. 创建 `BP_Campfire` 或 `Prefab_Campfire`。
2. 添加火堆模型或简单几何体。
3. 添加 `AkGameObj` 和 `AkEvent`。
4. Event 选择 `Play_OBJ_Campfire`。
5. 将 Prefab 放入室内 Room 内。
6. 确认它能收到室内混响。

### 4.6 四种地表

1. 创建四个 Unity Material：`MAT_Dirt`、`MAT_Rock`、`MAT_Wood`、`MAT_Grass`。
2. 创建四个 Physic Material：`PM_Dirt`、`PM_Rock`、`PM_Wood`、`PM_Grass`。
3. 场景中创建四块地面，分别赋予材质和 Collider。
4. 每个 Collider 绑定对应 Physic Material。

### 4.7 Footstep Notify / Animation Event

1. 在跑步动画左右脚落地帧添加 Animation Event。
2. Event 函数名建议：`OnFootstep`.
3. 脚步播放位置使用脚部 Socket 或脚部 Transform。
4. `OnFootstep` 中从脚底向下 Raycast。
5. 根据命中的 Physic Material 判断 `Dirt/Rock/Wood/Grass`。
6. 调用 Wwise：
   - 设置 `ShoeType` Switch。
   - 设置 `SurfaceType` Switch。
   - Post `Play_FOL_Footstep_Run` Event。

## 5. 验收清单

- Play in Editor 不报错。
- 室外能听到森林基底环境声。
- 走近门洞时室内外声音有空间过渡。
- 进入室内后混响明显但不过量。
- 室内篝火是 3D 声源，远近变化正常，并带混响。
- 角色跑步时脚步随动画触发。
- 脚步能根据 Dirt/Rock/Wood/Grass 切换声音。
- Wwise Profiler 能看到 Event、Switch、Room、Portal、Aux Send 正常工作。
- 总体响度听感控制在舒适范围，参考 -27 至 -40 LUFS。
