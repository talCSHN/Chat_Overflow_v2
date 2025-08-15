# Chat_Overflow_v2
[팀프로젝트 채팅프로그램](https://github.com/DarkCircle-chatApp-server) 리팩토링 버전
- C++ 기반 채팅 서버 → C# WPF 비동기 TCP 채팅 앱 리팩토링 프로젝트
- 실시간 채팅과 사용자 간 커뮤니케이션을 안정적으로 제공하는 Windows 데스크탑 기반 채팅 프로그램

## 프로젝트 개요
C++기반으로 Redis Pub/Sub을 통해 메시지를 주고받았던 프로그램을 C#과 WPF 기반 TCP/IP 비동기 채팅 구조로 리팩토링
IOCP 기반 비동기 구조와 싱글톤 구조로 설계하여 안정성과 확장성을 강화했습니다.

## 리팩토링 내용

| 항목           | Chat Overflow v1         | Chat Overflow v2                 |
|----------------|--------------------------|----------------------------------|
| 기술 스택      | C++, Redis, React              | C# WPF                          |
| 통신 구조      | Redis Pub/Sub      | 비동기 TCP/IP 소켓 (IOCP 기반)   |
| 처리 모델      | Thread-per-client        | Thread Pool + Async/Await        |
| 메시지 처리    | 무한 수신 루프      | 이벤트 기반 메시지 수신          |
| 자원 효율성    | 스레드 낭비 | 1~2개의 스레드만으로 모든 클라이언트 접속처리 가능      |


## 주요 기능

- 사용자 로그인: LOGIN Username SeatNumber 명령으로 접속

- 채팅 메시지 전송: CHAT Username SeatNumber Message 전송 및 브로드캐스팅

- 참가자 목록 관리: ObservableCollection을 통한 자동 UI 업데이트

- 이벤트 기반 메시지 수신: 콜백 구조로 메시지를 수신할 때마다 UI에 반영

- 비동기 서버/클라이언트 구조: Blocking 없는 소켓 처리

## 프로젝트 구조
```cmd
Chat_Overflow_v2/
├── Server/
│   ├── ServerSocket.cs         # 비동기 소켓 서버
│   ├── ChatServer.cs           # 채팅 처리 로직
│   └── Program.cs              # 프로그램 실행
├── Client/
│   ├── Models/
│   │   └── ClientSocket.cs     # TCP 클라이언트 소켓 생성 처리
│   ├── ViewModels/
│   │   └── ChatViewModel.cs    # 메시지 바인딩 처리
│   ├── Views/
│   │   └── ChatView.xaml       # UI 화면
│   └── App.xaml.cs             
```

## 향후 개선점

- 사용자 인증 및 세션 관리 강화

- 메시지 기록 DB 저장 기능 추가

- 파일 전송 및 이모지 지원
