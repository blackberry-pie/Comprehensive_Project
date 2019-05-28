/*--------------------------------------------------------------------------
	File-name	: index-text.c
	Description	: Example of calling Korean automatic indexing module.
			  Hangul I/O code is KSC-5601-1987 or KSSM Hangul code.
	Written-by	: Kang, Seung-Shik at Kookmin University  2001. 6.
--------------------------------------------------------------------------*/
#include <stdlib.h>

#include "../header/ham-ndx.h"
#include "../header/ham-api.h"
#include "../header/summary.h"

/*========================== Global Variable ===========================*/
HAM_TERMLIST Term[MAX_TERMS_DOC];	/* extracted terms */
HAM_TERMMEMORY TM;	/* termtext.h: memories needed for term extraction */
/*======================================================================*/

/* 입력 파일 전체를 memory로 load */
unsigned char *load_text(fp)
FILE *fp;
{
	long n;
	unsigned char *p;

	fseek(fp, 0L, 2);
	n = ftell(fp);	/* n: byte size of file 'fp' */

	fseek(fp, 0L, 0);
	p = (unsigned char *) malloc(n+1);	/* memory allocation */
	if (p == NULL) return NULL;

	fread(p, sizeof(unsigned char), n, fp);	/* read 'fp' to 'p' */
	*(p+n) = '\0';

	return p;
}

/* 추출된 용어 출력 */
void put_terms(fp, term, n, ts)
FILE *fp;
HAM_TERMLIST term[];/* 추출된 용어들의 정보 테이블 */
int n;				/* 추출된 용어 개수 */
unsigned char ts[];	/* 용어들이 NULL 문자 '\0'으로 구분되어 저장되어 있는 memory pool */
		/* ts[] --> "term1<null>term2<null>term3<null>...termN<null>" */
		/* term[i].offset --> 용어 i의 ts[] 저장 위치를 가리키는 정수 */
{
	int i, j, nlocs;

	fprintf(fp, "----------------------------------------------------------------------\n");
	fprintf(fp, " No: Freq  Score  Term\t\t Loc1\t Loc2\t Loc3\t Loc4\t Loc5\n");
	fprintf(fp, "----------------------------------------------------------------------\n");

	for (i = 0; i < n; i++) {
		fprintf(fp, "%3d:%5u %6u  %s", i+1,
			term[i].tf,			/* 용어의 빈도수 */
			term[i].score,		/* 용어의 가중치 */
			ts+term[i].offset);	/* ts+term[i].offset : 용어 스트링 */

			if (strlen(ts+term[i].offset) < 6) fprintf(fp, "\t");	/* 열 맞춤 */

		nlocs = (term[i].tf > MAX_LOCS_PER_TERM ? MAX_LOCS_PER_TERM : term[i].tf);
		for (j = 0; j < nlocs; j++)	/* 용어의 위치정보 최대 MAX_LOCS_PER_TERM 개 */
			fprintf(fp, "\t%5u", term[i].loc[j]);
		fprintf(fp, "\n");
	}

	fprintf(fp, "----------------------------------------------------------------------\n");
}

void usage_index()	/* HAM options in command-line argument */
{
	WELCOME;
	puts("usage: index [-options] [input.txt] [output.txt]\n");
	puts("    no options & I/O files --> default options applied");
	puts("\t-x: analysis results: default options");
	puts("\t-k: exclude SOME SINGLE NOUNS IN DIC.");
	puts("\t-K: exclude MOST SINGLE NOUNS IN DIC.");
	puts("\t-j: normalize Arabic/Hangul numerals\n");
	puts("\t-A: automatic spacing for input sentence");

	puts("\t-1: include 1-syllable nouns");
	puts("\t-9: ignore more than 9-syllable nouns");
	puts("\t-a: include all nouns with alphanumerics");
	puts("\t-V: include VERBs, ADVs, DETs, at. al.");
	puts("\t-u: include secondary candidate for unknown");
	puts("\t-v: include unknown guessed for analed word");
	puts("\t-c: exclude c-noun components");
	puts("\t-C: include 2 adjacent c-noun components");
	puts("\t-d: append c-noun components for max 2 candidates ");
	puts("\t-p: don't apply stopword list\n");
	puts("\t-i: extract input string itself as index-term");
	puts("\t-I: extract input string itself & not duplicated\n");
	puts("\t-2: 2nd type of output format");
	puts("\t-s: don't echo-back input sentence itself");
	puts("\t-w: don't echo-back input word itself");
	puts("\t-l: input mode: line-by-line --> sentence mark\n");
	puts("    Options may be combined like -xa, -x19a or -xsw0");
	puts("    WITH OPTION & NO I/O FILES SPECIFIED --> INTERACTIVE TESTING");
	GOODBYE;	/* goodbye message: 'header/ham-ndx.h' */
}

/*
	Set I/O types: following 3 types are possible.
		1. stdin & stdout
		2. file input & stdout
		3. file input & file output
*/
int set_iofile_ptr(argc, argv, fpin, fpout)
int argc; char *argv[];
FILE **fpin, **fpout;
{
	int i=1, nargs=argc;

	if (argc > 1 && argv[1][0] == '-') { i++; nargs--; }

	switch (nargs) {
	case 1:
		if (argc == 1) { usage_index(); return 1; }
		break;
	case 2:
		*fpin = fopen(argv[i], "rb");
		if (!*fpin) { printf("No such file <%s>\n", argv[i]); return 1; }
		break;
	case 3:
		*fpin = fopen(argv[i], "rb");
		if (!*fpin) { printf("No such file <%s>\n", argv[i]); return 1; }
		if (*fpout = fopen(argv[i+1], "r")) {
			fprintf(stderr, "Overwrite output file <%s> ? ", argv[i+1]);
			if (tolower(fgetc(stdin)) != 'y') return 1;
		}
		*fpout = fopen(argv[i+1], "w");
		break;
	default:
		usage_index(); return 1;
	}

	return 0;
}

void error_message(mode)	/* cannot load dictionary files */
HAM_RUNMODE mode;
{
	fprintf(stderr, "ERROR: cannot load dictionary files in <%s>. Error code %d in <header/err-code.h>\n",
		mode.dicpath, mode.err_code);
	fprintf(stderr, "Directory path for dictionaries should be correctly positioned. Or\n");
	fprintf(stderr, "you should modify <src/index.c> and <%s>.\n", INI_FILENAME);
	fprintf(stderr, "1. Check or modify the value <DicDirPath> in <%s>.\n", INI_FILENAME);
	fprintf(stderr, "2. Check or modify <src/index.c> --> function open_HAM() --> argument <%s>\n", INI_FILENAME);
}

/* 문장 추출 요약 결과 출력 */
void put_sent_score(HAM_SUMMARY *ab)
{
	int i;

	printf(" SID|Term#|Score| Sentence\n");
	printf("+---+-----+-----+-------------------------------------------------\n");
	for (i=0; i < ab->nsent; i++) {
		printf("%4d|%5d|%5d| %s\n", ab->sid[i], ab->nterm[i], ab->score[i], ab->sent[i]);
	}
}

/*
	각 문장에 대한 '점수' or '문장번호'로 sorting
		- flag : 0 -- '문장번호'순, 1 -- '점수'순 
*/
void sort_by_score(HAM_SUMMARY *ab, int n, int flag)
{
	int i, j, k, res;
	char *p;

	for (i=0; i < n-1; i++) {
		for (j=i+1; j < n; j++) {
			if (flag)
				res = (ab->score[i] < ab->score[j]);
			else res = (ab->sid[i] > ab->sid[j]);

			if (res) {
				p = ab->sent[i];
				ab->sent[i] = ab->sent[j];
				ab->sent[j] = p;

				k = ab->sid[i];
				ab->sid[i] = ab->sid[j];
				ab->sid[j] = k;

				k = ab->nword[i];
				ab->nword[i] = ab->nword[j];
				ab->nword[j] = k;

				k = ab->nterm[i];
				ab->nterm[i] = ab->nterm[j];
				ab->nterm[j] = k;

				k = ab->wbase[i];
				ab->wbase[i] = ab->wbase[j];
				ab->wbase[j] = k;

				k = ab->score[i];
				ab->score[i] = ab->score[j];
				ab->score[j] = k;

				k = ab->term1[i];
				ab->term1[i] = ab->term1[j];
				ab->term1[j] = k;
			}
		}
	}
}

/*
	Example of calling keyword extraction routine.

	Arguments of 'get_stems()'
		char *sent;   --> input sentence(KSC 5601 Hangul code)
		char *keys[]; --> pointer array of keywords.

	Two more arguments are needed in 'get_stems_TS()'
		char *KeyMem; --> keywords saving area.
		HAM_MORES HamOut; --> morph. analysis result

	Return value of 'get_stems' and 'get_stems_TS()'
		is the number of keywords in 'keys[]'.

	You may use 'get_stems()' or 'get_stems_TS()' as you want.
	Only the difference is the number of arguments & Thread-Safe.
*/

/*---------- INTEGRATION GUIDE of HAM to app. system -----------*/
/*                                                              */
/* 0. You should include 'header/ham-ndx.h'.                    */
/*                                                              */
/* 1. Following variables are required for HAM                  */
/*       char text[MAX];  -->  KSC5601 input Korean text        */
/*       char *terms[MAX_TERMS]; -->  ptr. array of terms       */
/*       HAM_RUNMODE mode  -->  running mode of HAM             */
/*                                                              */
/*       char KeyMem[MAXKEYBYTES]; -->  keyword saving area     */
/*       HAM_MORES HamOut;  -->  morph. analysis result         */
/*                                                              */
/* 2. Call 'open_HAM_index(&mode, option_string, filename)'     */
/*       before 1st call get_stems() or get_stems_TS().         */
/*       'userdic' is a user-dic name: default "ham-usr.dic".   */
/*       It's argument is a directory path of 'hangul.???'.     */
/*       It initializes global var.s and opens dic. files.      */
/*       Return value --- 0(normal), 1(dic.file open failure)   */
/*                                                              */
/* 3. Call 'get_terms(text, terms, mode)'.                      */
/*       Extracted keywords(nouns) from the sentence are        */
/*       confirmed by 'put_terms(fpout, sent, keys, nkeys)'.    */
/*                                                              */
/* 4. Call 'close_HAM()', if get_terms?() is no nonger used.    */
/*                                                              */
/* Only 3 functions are required for integration to appl. system*/
/*        open_HAM_index(), get_terms(), close_HAM()            */
/*                                                              */
/*--------------------------------------------------------------*/
main(int argc, char *argv[])
{
	FILE *fpin=stdin, *fpout=stdout;
	char *optstr=NULL;	/* option string: e.g. "1apVC", "pVc" */
	HAM_RUNMODE mode;	/* HAM running mode: 'header/runmode.h' */

	unsigned char *text;	/* input text: 입력 파일 전체 */
	int n, n2=0;	/* 추출된 용어수: 명령행 인자로 용어의 최대 개수 지정 */

	HAM_SUMMARY abs;		/* setence extraction result */
	
	if (argc > 1 && argv[1][0] == '-')
		n = atoi(argv[1]+1);	/* 출력할 term의 최대 개수 지정 */
	/*if (argc > 1 && argv[1][0] == '-') optstr = argv[1]+1;*/

	if (set_iofile_ptr(argc, argv, &fpin, &fpout))
		return 0;   /* I/O files open failed */

	/*=========================== Initialization ===========================*/
	if (open_HAM_index(&mode, optstr, "./hdic/KLT2000.ini")) {
		error_message(mode); return 0;
	} else WELCOME;	/* welcome message: 'header/ham-ndx.h' */
	/*======================================================================*/

	/*=========================== Extract Sents ============================*/
	if ((text=load_text(fpin)) == NULL)	/* [주의] 'fpin'은 "rb"로 open */
		return -1;	/* text file --> 'text' */

	n = sentence_extraction(text, &abs, Term, &TM, &mode);
	sort_by_score(&abs, abs.nsent, 1);	/* 1: 점수별 소팅 */
	//sort_by_score(&abs, abs.nsent, 0);	/* 0: 문장 순서대로 소팅 */

	put_sent_score(&abs);	/* 문장 추출 요약 결과 출력 */
//	put_terms(fpout, Term, n, TM.memTermString);	/* 용어 추출 결과 출력 */

	/*========================= Finalize Indexer ===========================*/
	close_HAM_index(&mode);	/* HAM is not used any more */
	/*======================================================================*/

	free(text);	/* malloc() in 'load_text()' */
	if (fpin != stdin) fclose(fpin); if (fpout != stdout) fclose(fpout);
	GOODBYE;	/* good-bye message: 'header/ham-ndx.h' */
	return 0;
}
/*---------------------------- end of index-text.c --------------------------*/
