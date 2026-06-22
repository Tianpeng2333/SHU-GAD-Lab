# SHU-GAD-Lab

Unity + Wwise 游戏音频设计课程作业项目。项目实现了一个第三人称可交互音频场景，重点展示 Wwise 在 Unity 中的环境声、3D 物件声、角色 Foley、Room/Portal 空间音频与室内外过渡。

## 项目内容

- 空间音频：使用 `AkRoom` 与 `AkRoomPortal` 搭建室内外空间连接。
- 环境声：森林室外 Ambience，包含 2D 底噪和 3D 随机自然声。
- 3D 物件：室内篝火声，包含燃烧 Loop、Crack、Sizzle 随机细节。
- 角色脚步：通过动画事件触发 Wwise Foley，并根据地表切换声音。
- 材质检测：使用 Raycast 检测 Dirt、Grass、Rock、Wood 四种地表。
- 室内外过渡：通过 `RTPC_PlayerIndoor` 控制进入室内后的环境声变化。
- 室内混响：使用 `AUX_Room_Reverb` 与 Wwise RoomVerb 表现室内空间感。

详细作品说明见：[Report/Report.md](Report/Report.md)。打包后的 Windows 平台可执行文件及 Demo 视频可在 [Release 页面](https://github.com/Tianpeng2333/SHU-GAD-Lab/releases)下载。

## 项目结构

```text
.
├── SHU_GAD_WwiseLab/                         # Unity 项目
│   ├── Assets/
│   │   ├── Scenes/Main.unity                 # 主场景
│   │   ├── Scripts/                          # 自定义 Wwise 交互脚本
│   │   ├── StreamingAssets/Audio/             # Wwise Generated SoundBanks
│   │   └── Wwise/                            # Wwise Unity Integration 资源
│   ├── Packages/
│   ├── ProjectSettings/
│   └── SHU_GAD_WwiseLab_WwiseProject/         # Wwise 工程
├── Sound/                                    # 原始音频素材
├── Report/                                   # 作品说明报告与图片
├── Unity_Wwise_Audio_Design_Assignment_Breakdown.md
├── Unity_Wwise_Assignment_Implementation_Guide.md
└── .gitignore
```

## 环境要求

- Unity：高于 2022.3 的版本，建议使用 Unity 2022 LTS 以获得更好的稳定性和性能。
- Wwise：建议使用与工程集成一致的 Wwise 2025 LTS 版本。
- 平台：当前工程主要面向 Windows 编辑器运行。

## 运行方式

1. 用 Unity Hub 打开 `SHU_GAD_WwiseLab`。
2. 打开主场景：

```text
SHU_GAD_WwiseLab/Assets/Scenes/Main.unity
```

3. 如 SoundBank 缺失或事件无法播放，打开 Wwise 工程：

```text
SHU_GAD_WwiseLab/SHU_GAD_WwiseLab_WwiseProject/SHU_GAD_WwiseLab_WwiseProject.wproj
```

4. 在 Wwise 中执行 Generate SoundBanks。
5. 回到 Unity，确认 Wwise 资源刷新，单击菜单栏中的 `Tools-Wwise-Clear User-Defined SoundBank On All Events` 后再进入 Play Mode。后续进入 Play Mode 时，也需先执行一次 `Clear User-Defined SoundBank On All Events` 来确保事件正确加载 SoundBank。