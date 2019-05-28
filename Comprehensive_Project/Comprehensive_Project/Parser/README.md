http://nlp.kookmin.ac.kr/에서 다운받아 사용하던 형태소 분석기가 사용기간 만료로 인하여 사용할 수 없으면 여기에 올린 DLL로 교체하여 정상적으로 사용이 가능합니다.



첨부파일 KLT2000-TEST.dll



이 DLL은 2020년 12월 31일까지 사용이 가능합니다.

DLL 포함하여 실행파일 등 포함된 2017~2018년도 사용 가능한 파일을 아래에 첨부하였습니다.



첨부파일 KLT2010-TestVersion-2017.zip

 

<사용법> cmd창에서 실행해야 합니다.

   압축을 풀면 API, EXE, Project라는 폴더가 있습니다. EXE 폴더 안에 확장자가 .exe 인 파일들이 있습니다.

   이 중에서 index.exe 또는 indexT.exe를 실행하면 조사/어미 등을 제거한 결과를 얻을 수 있습니다.

   예를 들어, 입력 파일 "t1.txt"(폴더에 포함되어 있음)에 대해 아래와 같이 실행하면 됩니다.



   C> cd EXE


   C> index.exe t1.txt

   C> index.exe t1.txt output.txt

          // 실행결과가 output.txt에 저장됨. 탐색기에서 output.txt를 열어서 결과 확인.



   C> indexT.exe t1.txt

   C> indexT.exe t1.txt output.txt


<참고> 2015/01/09 -- 새로운/간편한 인터페이스 함수 추가하였음

- 사전 로딩 초기화 함수를 형태소 분석기 내부에서 처리

- API/index/src/indexS-simple.c 를 이용하여 실행파일 생성 --> EXE/indexNew.exe

- indexNew.exe 생성 방법

     Project/EXE/EXE.dsw에서 indexS.c를 삭제하고 indexS-simple.c를 추가하여 Build