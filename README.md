# SnowHero

## 발견된 버그 및 문제점
- 마법사가 때때로 스킬을 2번 연속 사용
- 마법사 스킬 애니메이션 어색


## 메모
- 패키지 매니저에서 `Oculus Integration` 설치해서 임포트 부탁드려요! 용량이 커서 혹시몰라 이그노어 처리했습니다. 아직 사용된 파일은 X

- 샘플 씬에서 Player에 있는 Cube는 플레이어가 쳐다보는 방향 표시를 위한 오브젝트입니다. enemy가 아니에요!

- 참고!
https://wergia.tistory.com/238

- 주의점!
https://blog.naver.com/raruz/222852771902

- 유니티 버전
`2021.3.21f1`



## 수정 내역
### [2023-05-28]
`v0.0.1` (13:40)
- 스크립트 `CamRotate`, `PlayerMove`와 `ARAVRInput` 파일 추가 
- ~~스크립트 `GrabObject`, `Gun`은 참고용으로 추가 (추후 삭제 할 수도?)~~
- 스크립트 `EnemyFSM` 추가
    플레이어 추격, 공격 등

`v0.0.2` (19:00)
- `EnemyFSM`의 플레이어 추격 부분 수정
- 스테이지와 UI에 관련된 `StageScripts/` 내에 스크립트 `Mainmenu`, `StageButtonEvent`, `StageLock` 추가
- 전체적인 게임 정보(스테이지 진행)를 다루는 `GameManager` 추가
- `Scenes/` 내에 스테이지 씬 생성


### [2023-05-29]
`v0.0.3` (13:20)
- 스크립트 `Player` 추가
- 무한 점프 수정
- 플레이어 체력, enemy의 플레이어 공격 추가

`v0.0.4` (16:15)
- UI 리소스추가
- 메인/스테이지/UI 제작중 (아직미완)

`v0.0.5` (19:30)
- `v0.0.3`에 작업한 내용 사라져서 다시 추가
- ~~테스트 할 때마다 프로젝트 자주 다운됨......~~

`v0.0.6` (20:45)
- Enemy 네비게이션 기능 수정

`v0.0.7` (22:50)
- 스크립트 `SnowballThrower`, `DestroySnowball` 파일  추가


### [2023-06-01]
`v0.0.8` (11:03)
- SampleScenes, 1Stage 꾸미기

`v0.0.9` (13:07)
- Audio, Effect, Item, Monster 에셋 추가


### [2023-06-03]
`v0.0.10` (15:40)
- `GrabObject`, `Gun` 스크립트 삭제
- `Oculus Integration` 패키지 설치 필요
- `Asset/Oculus/` 이그노어 등록


### [2023-06-03]
`v0.1.0` (22:06)
- `MainMenu`, `StageButtonEvent`, `StageLock` , `Upgrade` 스크립트 수정 및 추가
    ~~->  `Upgrade` 스크립트 별의 개수만큼 업그레이드 할수있도록 수정예정~~ 
- `UI` 디자인 리소스가 추가, 변경되었습니다.
- PlayerPref에 Int형 변수 "stage" , "stage_star_x" 추가 
    -> stage 0 클리어시 stage변수를 1로 바꿔주시면됩니다. 
    -> PlayerPrefs.SetInt(stage_star_0,3) -> 0번스테이지 별 3개 주겟다는 의미입니다. 
- PlayerPref에 Int형 변수 MoveSpeed , AttackSpeed , Health , AttackPower , SpecialSkill 추가되었습니다. 
    -> Player 에서 GetInt 하셔서 더해주시면 될것같습니다.

`v0.1.1` (22:55)
- 조작키를 볼수있는 UI 추가
- `Upgrade` 스크립트 별의 개수만큼 업그레이드 할수있도록 수정
- PlayerPref에 Int형 변수 "total_star" 추가 
- `GameManager` 참고하여 SampleScene에 UI 추가하겠습니다. 


### [2023-06-04]
`v0.1.2` (17:10)
- `EnemyFSM` 스크립트 수정 및 추가
    -> 보스 패턴 추가
- `SnowballThrower` 스크립트 수정
- `DestroySnowball` 스크립트 수정
- `Magic_ball`, `Magic_spear`, `Magic_rock` 스크립트 추가
    -> 보스 공격 스킬 수정 및 추가 예정

`v0.1.3` (18:35)
- `EnemyFSM` 스크립트에 애니메이션 부분 추가
- `Enemy/` 내에 완성된 enemy 객체 `Dog` 및 애니메이터 `DogAnim` 추가
- `HitEvent` 스크립트 추가

### [2023-06-04]
`v0.1.4` (21:27)
- `GameManager`에서 게임클리어 , 죽었을경우 UI 출력 및 버튼연결 추가, 스테이지 관련 스크립트 완료
- `Player` 에서 피격 이펙트 , 체력바 가 추가되었습니다.

### [2023-06-05]
`v0.1.5` (16:50)
- `Enemy/` 내에 완성된 enemy 객체 `Cactus` 및 애니메이터 `CactusAnim` 추가
- 스크립트 `Boss_Wizard`, `BossSkill` 추가
- `EnemyFSM` 수정

### [2023-06-06]
`v0.1.6` (15:35)
- `EnemyFSM`, `Boss_Wizard` 수정 : 보스가 이제 스킬을 제자리에서 사용!
- `BossWizardAnim` 수정, 추가 수정 필요
- `Enemy/` 내에 완성된 enemy 객체 `Slime` 9종, `Mushroom` 및 애니메이터 `SlimeAnim`, `MushroomAnim`추가
- enemy 경직 추가



## 스크립트 정보
- `CamRotate`
    플레이어의 화면 회전(마우스)

- `EnemyFSM`
    적의 상태, 정보

- `GameManager`
    게임 진행 요소를 관리하는 스크립트

- `HitEvent`
    플레이어 피격 이벤트 관리

- `Mainmenu`
    메인 메뉴

- `Player`
    플레이어 체력 관리

- `PlayerMove`
    플레이어의 이동

- `PlayerRotate`
    플레이어 캐릭터의 회전

- `StageButtonEvent`
    버튼 상호작용 이벤트 처리

- `StageLock`
    스테이지 해방 기능

- `SnowballThrower`
    눈덩이 던지가 기능

- `DestroySnowball`
    눈덩이 적중시 눈덩이 삭제 및 데미지 기능

- `Upgrade` 
    Player업그레이드 스크립트 

- `Magic_ball`
    보스 마법 1
- `Magic_spear`
    보스 마법 2
- `Magic_rock`
    보스 마법 3
