# SnowHero

## 메모
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
- 스크립트 `GrabObject`, `Gun`은 참고용으로 추가 (추후 삭제 할 수도?)
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

## 스크립트 정보
- `CamRotate`
    플레이어의 화면 회전(마우스)

- `EnemyFSM`
    적의 상태, 정보

- `GameManager`
    게임 진행 요소를 관리하는 스크립트

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