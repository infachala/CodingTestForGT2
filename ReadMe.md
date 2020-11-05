## 프로그램 설명
본 프로그램은 FTP, HTTP 프로토콜을 이용하여 특정 데이터를 서버로부터 로컬로 다운로드 하는 기능을 수행한다.


## 주요 기능

### FTP 다운로더
1. FTP 접속 정보를 입력 받음
   1. Address / ID / PW 을 받아야 함
   1. Remote Path 를 받아야 함
1. Local Folder Path 를 입력 받음
1. FTP Remote Path 의 모든 파일을 Local Folder Path 로 다운로드 받음
   1. FTP Remote Path 가 파일이라면 동일 폴더에 존재하는 모든 파일을 다운로드
   1. FTP Remote Path 가 폴더라면 해당 폴더에 존재하는 모든 파일을 다운로드
1. 다운로드 결과를 출력

### HTTP 다운로더
1. HTTP 접속 정보를 입력 받음
   1. URL 을 입력 받음
1. Local File Path 를 입력 받음
1. HTTP 접속 정보에 해당하는 파일을 Local File Path 로 다운로드 받음
1. 다운로드 결과를 출력


## 테스트 설명
### 작업 진행 흐름
1. Develop Branch 에서 본인 작업 Branch(B1) 생성
1. B1 으로부터 FTP 다운로더 구현을 위한 Branch(B2) 생성
1. B1 으로부터 설정 파일 적용을 위한 Branch(B3) 생성
1. B2 에서 FTP 다운로더 구현 후, B1 에 Merge
1. B3 에서 FTP 접속 정보를 파일(XML or JSON)로 인터페이스 하도록 수정 후, B1 에 Merge
   1. 충돌 발생 시, 해결

### 테스트 시 사용할 기본 접속 정보
1. FTP 접속 Server
   1. ftp://ftp.iers.org/products/eop/bulletinb/format_2009/
1. HTTP 접속 URL
   1. http://www.celestrak.com/NORAD/elements/tle-new.txt

### 추가 점수 부여
1. Unit Test Code 작성
1. 기본 제공 프로그램 Refactoring


### 유의 사항
1. Open Source 를 이용해도 무관함. 단, 어떤 식으로 동작하는 것인지 이해해야 하며 질문 시, 설명 가능해야 함
1. 질문이 있을 시, gtl@satreci 으로 문의
