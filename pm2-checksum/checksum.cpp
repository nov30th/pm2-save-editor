#ifndef PM2_CHECKSUM
#include "checksum.h"
#define PM2_CHECKSUM
#endif

extern "C" __declspec(dllexport) int CalculateChecksum(pm2_save_file_skeleton *pm2_file, int version) {

		int i;
		int new_checksum = 0;

		new_checksum += pm2_file->variable_0001;
		new_checksum += pm2_file->variable_0002;
		new_checksum += pm2_file->variable_0003;
		new_checksum += pm2_file->variable_0004;
		new_checksum += pm2_file->variable_0005;
		new_checksum += pm2_file->variable_0006;
		new_checksum += pm2_file->variable_0007;
		new_checksum += pm2_file->variable_0008;
		new_checksum += pm2_file->variable_0009;
		new_checksum += pm2_file->variable_0010;
		new_checksum += pm2_file->variable_0011;
		new_checksum += pm2_file->variable_0012;
		new_checksum += pm2_file->variable_0013;
		new_checksum += pm2_file->variable_0014;

		new_checksum += pm2_file->variable_0015;
		new_checksum += pm2_file->variable_0016;
		new_checksum += pm2_file->variable_0017;
		new_checksum += pm2_file->variable_0018;
		new_checksum += pm2_file->variable_0019;
		new_checksum += pm2_file->variable_0020;
		new_checksum += pm2_file->variable_0021;
		new_checksum += pm2_file->variable_0022;
		new_checksum += pm2_file->variable_0023;
		new_checksum += pm2_file->variable_0024;
		new_checksum += pm2_file->variable_0025;
		new_checksum += pm2_file->variable_0026;
		new_checksum += pm2_file->variable_0027;
		new_checksum += pm2_file->variable_0028;
		new_checksum += pm2_file->variable_0029;

		new_checksum += pm2_file->variable_0030;
		new_checksum += pm2_file->variable_0031;
		new_checksum += pm2_file->variable_0032;
		new_checksum += pm2_file->variable_0033;
		new_checksum += pm2_file->variable_0034;
		new_checksum += pm2_file->variable_0035;
		new_checksum += pm2_file->variable_0036;
		new_checksum += pm2_file->variable_0037;
		new_checksum += pm2_file->variable_0038;
		new_checksum += pm2_file->variable_0039;
		new_checksum += pm2_file->variable_0040;
		new_checksum += pm2_file->variable_0041;
		new_checksum += pm2_file->variable_0042;
		new_checksum += pm2_file->variable_0043;
		new_checksum += pm2_file->variable_0044;
		new_checksum += pm2_file->variable_0045;
		new_checksum += pm2_file->variable_0046;
		new_checksum += pm2_file->variable_0047;
		new_checksum += pm2_file->variable_0048;
		new_checksum += pm2_file->variable_0049;
		new_checksum += pm2_file->variable_0050;
		new_checksum += pm2_file->variable_0051;
		new_checksum += pm2_file->variable_0052;
		new_checksum += pm2_file->variable_0053;
		new_checksum += pm2_file->variable_0054;
		new_checksum += pm2_file->variable_0055;
		new_checksum += pm2_file->variable_0056;

		new_checksum += pm2_file->variable_0057;

		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0058[i];

		}

		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0059[i];

		}

		new_checksum += pm2_file->variable_0060;
		new_checksum += pm2_file->variable_0061;

		new_checksum += pm2_file->variable_0062;
		new_checksum += pm2_file->variable_0063;

		new_checksum += pm2_file->variable_0064;
		new_checksum += pm2_file->variable_0065;

		new_checksum += pm2_file->variable_0066;
		new_checksum += pm2_file->variable_0067;

		new_checksum += pm2_file->variable_0068;
		new_checksum += pm2_file->variable_0069;

		new_checksum += pm2_file->variable_0070;
		new_checksum += pm2_file->variable_0071;

		new_checksum += pm2_file->variable_0072;
		new_checksum += pm2_file->variable_0073;



		new_checksum += pm2_file->variable_0074;
		new_checksum += pm2_file->variable_0075;
		new_checksum += pm2_file->variable_0076;
		new_checksum += pm2_file->variable_0077;
		new_checksum += pm2_file->variable_0078;
		new_checksum += pm2_file->variable_0079;
		new_checksum += pm2_file->variable_0080;
		new_checksum += pm2_file->variable_0081;
		new_checksum += pm2_file->variable_0082;
		new_checksum += pm2_file->variable_0083;
		new_checksum += pm2_file->variable_0084;
		new_checksum += pm2_file->variable_0085;


		new_checksum += pm2_file->variable_0086;
		new_checksum += pm2_file->variable_0087;
		new_checksum += pm2_file->variable_0088;
		new_checksum += pm2_file->variable_0089;
		new_checksum += pm2_file->variable_0090;
		new_checksum += pm2_file->variable_0091;
		new_checksum += pm2_file->variable_0092;
		new_checksum += pm2_file->variable_0093;
		new_checksum += pm2_file->variable_0094;
		new_checksum += pm2_file->variable_0095;


		new_checksum += pm2_file->variable_0096;
		new_checksum += pm2_file->variable_0097;
		new_checksum += pm2_file->variable_0098;
		new_checksum += pm2_file->variable_0099;
		new_checksum += pm2_file->variable_0100;
		new_checksum += pm2_file->variable_0101;
		new_checksum += pm2_file->variable_0102;
		new_checksum += pm2_file->variable_0103;
		new_checksum += pm2_file->variable_0104;
		new_checksum += pm2_file->variable_0105;
		new_checksum += pm2_file->variable_0106;
		new_checksum += pm2_file->variable_0107;



		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0108[i];

		}

		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0109[i];

		}

		new_checksum += pm2_file->variable_0110;
		new_checksum += pm2_file->variable_0111;



		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0112[i];

		}


		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0113[i];

		}



		new_checksum += pm2_file->variable_0114;
		new_checksum += pm2_file->variable_0115;


		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0116[i];

		}


		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0117[i];

		}



		new_checksum += pm2_file->variable_0118;
		new_checksum += pm2_file->variable_0119;

		for (i = 0; i < 50; i++) {

			new_checksum += pm2_file->variable_0120[i];

		}




		for (i = 0; i < 50; i++) {

			new_checksum += pm2_file->variable_0121[i];

		}


		for (i = 0; i < 50; i++) {

			new_checksum += pm2_file->variable_0122[i];

		}

		// There was a counting error here

		for (i = 0; i < 2400; i++) {

			new_checksum += pm2_file->variable_0198[i];

		}

		for (i = 0; i < 2400; i++) {

			new_checksum += pm2_file->variable_0199[i];

		}

		new_checksum += pm2_file->variable_0200;
		new_checksum += pm2_file->variable_0201;
		new_checksum += pm2_file->variable_0202;
		new_checksum += pm2_file->variable_0203;
		new_checksum += pm2_file->variable_0204;
		new_checksum += pm2_file->variable_0205;
		new_checksum += pm2_file->variable_0206;
		new_checksum += pm2_file->variable_0207;
		new_checksum += pm2_file->variable_0208;
		new_checksum += pm2_file->variable_0209;

		new_checksum += pm2_file->variable_0210;

		new_checksum += pm2_file->variable_0211;
		new_checksum += pm2_file->variable_0212;
		new_checksum += pm2_file->variable_0213;
		new_checksum += pm2_file->variable_0214;

		new_checksum += pm2_file->variable_0215;
		new_checksum += pm2_file->variable_0216;


		new_checksum += pm2_file->variable_0217;
		new_checksum += pm2_file->variable_0218;
		new_checksum += pm2_file->variable_0219;
		new_checksum += pm2_file->variable_0220;
		new_checksum += pm2_file->variable_0221;
		new_checksum += pm2_file->variable_0222;
		new_checksum += pm2_file->variable_0223;
		new_checksum += pm2_file->variable_0224;
		new_checksum += pm2_file->variable_0225;
		new_checksum += pm2_file->variable_0226;
		new_checksum += pm2_file->variable_0227;

		new_checksum += pm2_file->variable_0228;
		new_checksum += pm2_file->variable_0229;
		new_checksum += pm2_file->variable_0230;
		new_checksum += pm2_file->variable_0231;
		new_checksum += pm2_file->variable_0232;
		new_checksum += pm2_file->variable_0233;
		new_checksum += pm2_file->variable_0234;
		new_checksum += pm2_file->variable_0235;
		new_checksum += pm2_file->variable_0236;
		new_checksum += pm2_file->variable_0237;
		new_checksum += pm2_file->variable_0238;
		new_checksum += pm2_file->variable_0239;
		new_checksum += pm2_file->variable_0240;
		new_checksum += pm2_file->variable_0241;
		new_checksum += pm2_file->variable_0242;
		new_checksum += pm2_file->variable_0243;
		new_checksum += pm2_file->variable_0244;
		new_checksum += pm2_file->variable_0245;
		new_checksum += pm2_file->variable_0246;
		new_checksum += pm2_file->variable_0247;
		new_checksum += pm2_file->variable_0248;
		new_checksum += pm2_file->variable_0249;
		new_checksum += pm2_file->variable_0250;
		new_checksum += pm2_file->variable_0251;
		new_checksum += pm2_file->variable_0252;
		new_checksum += pm2_file->variable_0253;
		new_checksum += pm2_file->variable_0254;
		new_checksum += pm2_file->variable_0255;
		new_checksum += pm2_file->variable_0256;
		new_checksum += pm2_file->variable_0257;
		new_checksum += pm2_file->variable_0258;
		new_checksum += pm2_file->variable_0259;

		new_checksum += pm2_file->variable_0260;
		new_checksum += pm2_file->variable_0261;
		new_checksum += pm2_file->variable_0262;
		new_checksum += pm2_file->variable_0263;
		new_checksum += pm2_file->variable_0264;
		new_checksum += pm2_file->variable_0265;
		new_checksum += pm2_file->variable_0266;
		new_checksum += pm2_file->variable_0267;
		new_checksum += pm2_file->variable_0268;
		new_checksum += pm2_file->variable_0269;
		new_checksum += pm2_file->variable_0270;
		new_checksum += pm2_file->variable_0271;
		new_checksum += pm2_file->variable_0272;
		new_checksum += pm2_file->variable_0273;
		new_checksum += pm2_file->variable_0274;
		new_checksum += pm2_file->variable_0275;
		new_checksum += pm2_file->variable_0276;
		new_checksum += pm2_file->variable_0277;
		new_checksum += pm2_file->variable_0278;
		new_checksum += pm2_file->variable_0279;
		new_checksum += pm2_file->variable_0280;
		new_checksum += pm2_file->variable_0281;
		new_checksum += pm2_file->variable_0282;
		new_checksum += pm2_file->variable_0283;
		new_checksum += pm2_file->variable_0284;
		new_checksum += pm2_file->variable_0285;
		new_checksum += pm2_file->variable_0286;
		new_checksum += pm2_file->variable_0287;
		new_checksum += pm2_file->variable_0288;
		new_checksum += pm2_file->variable_0289;
		new_checksum += pm2_file->variable_0290;
		new_checksum += pm2_file->variable_0291;

		new_checksum += pm2_file->variable_0292;
		new_checksum += pm2_file->variable_0293;
		new_checksum += pm2_file->variable_0294;
		new_checksum += pm2_file->variable_0295;
		new_checksum += pm2_file->variable_0296;

		new_checksum += pm2_file->variable_0297;
		new_checksum += pm2_file->variable_0298;
		new_checksum += pm2_file->variable_0299;

		new_checksum += pm2_file->variable_0300;
		new_checksum += pm2_file->variable_0301;
		new_checksum += pm2_file->variable_0302;
		new_checksum += pm2_file->variable_0303;
		new_checksum += pm2_file->variable_0304;

		new_checksum += pm2_file->variable_0305;
		new_checksum += pm2_file->variable_0306;
		new_checksum += pm2_file->variable_0307;
		new_checksum += pm2_file->variable_0308;
		new_checksum += pm2_file->variable_0309;
		new_checksum += pm2_file->variable_0310;
		new_checksum += pm2_file->variable_0311;
		new_checksum += pm2_file->variable_0312;
		new_checksum += pm2_file->variable_0313;
		new_checksum += pm2_file->variable_0314;
		new_checksum += pm2_file->variable_0315;
		new_checksum += pm2_file->variable_0316;
		new_checksum += pm2_file->variable_0317;
		new_checksum += pm2_file->variable_0318;
		new_checksum += pm2_file->variable_0319;
		new_checksum += pm2_file->variable_0320;
		new_checksum += pm2_file->variable_0321;
		new_checksum += pm2_file->variable_0322;
		new_checksum += pm2_file->variable_0323;
		new_checksum += pm2_file->variable_0324;
		new_checksum += pm2_file->variable_0325;
		new_checksum += pm2_file->variable_0326;
		new_checksum += pm2_file->variable_0327;
		new_checksum += pm2_file->variable_0328;
		new_checksum += pm2_file->variable_0329;
		new_checksum += pm2_file->variable_0330;
		new_checksum += pm2_file->variable_0331;
		new_checksum += pm2_file->variable_0332;
		new_checksum += pm2_file->variable_0333;
		new_checksum += pm2_file->variable_0334;
		new_checksum += pm2_file->variable_0335;
		new_checksum += pm2_file->variable_0336;
		new_checksum += pm2_file->variable_0337;
		new_checksum += pm2_file->variable_0338;
		new_checksum += pm2_file->variable_0339;
		new_checksum += pm2_file->variable_0340;
		new_checksum += pm2_file->variable_0341;
		new_checksum += pm2_file->variable_0342;
		new_checksum += pm2_file->variable_0343;
		new_checksum += pm2_file->variable_0344;
		new_checksum += pm2_file->variable_0345;
		new_checksum += pm2_file->variable_0346;
		new_checksum += pm2_file->variable_0347;
		new_checksum += pm2_file->variable_0348;
		new_checksum += pm2_file->variable_0349;
		new_checksum += pm2_file->variable_0350;
		new_checksum += pm2_file->variable_0351;
		new_checksum += pm2_file->variable_0352;
		new_checksum += pm2_file->variable_0353;
		new_checksum += pm2_file->variable_0354;
		new_checksum += pm2_file->variable_0355;
		new_checksum += pm2_file->variable_0356;
		new_checksum += pm2_file->variable_0357;
		new_checksum += pm2_file->variable_0358;
		new_checksum += pm2_file->variable_0359;
		new_checksum += pm2_file->variable_0360;
		new_checksum += pm2_file->variable_0361;
		new_checksum += pm2_file->variable_0362;
		new_checksum += pm2_file->variable_0363;
		new_checksum += pm2_file->variable_0364;

		new_checksum += pm2_file->variable_0365;
		new_checksum += pm2_file->variable_0366;
		new_checksum += pm2_file->variable_0367;
		new_checksum += pm2_file->variable_0368;
		new_checksum += pm2_file->variable_0369;
		new_checksum += pm2_file->variable_0370;
		new_checksum += pm2_file->variable_0371;
		new_checksum += pm2_file->variable_0372;
		new_checksum += pm2_file->variable_0373;
		new_checksum += pm2_file->variable_0374;
		new_checksum += pm2_file->variable_0375;
		new_checksum += pm2_file->variable_0376;
		new_checksum += pm2_file->variable_0377;
		new_checksum += pm2_file->variable_0378;
		new_checksum += pm2_file->variable_0379;
		new_checksum += pm2_file->variable_0380;
		new_checksum += pm2_file->variable_0381;
		new_checksum += pm2_file->variable_0382;
		new_checksum += pm2_file->variable_0383;
		new_checksum += pm2_file->variable_0384;
		new_checksum += pm2_file->variable_0385;
		new_checksum += pm2_file->variable_0386;
		new_checksum += pm2_file->variable_0387;
		new_checksum += pm2_file->variable_0388;
		new_checksum += pm2_file->variable_0389;
		new_checksum += pm2_file->variable_0390;

		new_checksum += pm2_file->variable_0391;
		new_checksum += pm2_file->variable_0392;
		new_checksum += pm2_file->variable_0393;
		new_checksum += pm2_file->variable_0394;
		new_checksum += pm2_file->variable_0395;
		new_checksum += pm2_file->variable_0396;
		new_checksum += pm2_file->variable_0397;
		new_checksum += pm2_file->variable_0398;
		new_checksum += pm2_file->variable_0399;
		new_checksum += pm2_file->variable_0400;
		new_checksum += pm2_file->variable_0401;
		new_checksum += pm2_file->variable_0402;
		new_checksum += pm2_file->variable_0403;
		new_checksum += pm2_file->variable_0404;
		new_checksum += pm2_file->variable_0405;
		new_checksum += pm2_file->variable_0406;
		new_checksum += pm2_file->variable_0407;
		new_checksum += pm2_file->variable_0408;
		new_checksum += pm2_file->variable_0409;
		new_checksum += pm2_file->variable_0410;
		new_checksum += pm2_file->variable_0411;
		new_checksum += pm2_file->variable_0412;
		new_checksum += pm2_file->variable_0413;
		new_checksum += pm2_file->variable_0414;
		new_checksum += pm2_file->variable_0415;
		new_checksum += pm2_file->variable_0416;
		new_checksum += pm2_file->variable_0417;
		new_checksum += pm2_file->variable_0418;
		new_checksum += pm2_file->variable_0419;
		new_checksum += pm2_file->variable_0420;
		new_checksum += pm2_file->variable_0421;
		new_checksum += pm2_file->variable_0422;
		new_checksum += pm2_file->variable_0423;
		new_checksum += pm2_file->variable_0424;
		new_checksum += pm2_file->variable_0425;
		new_checksum += pm2_file->variable_0426;
		new_checksum += pm2_file->variable_0427;
		new_checksum += pm2_file->variable_0428;
		new_checksum += pm2_file->variable_0429;
		new_checksum += pm2_file->variable_0430;
		new_checksum += pm2_file->variable_0431;
		new_checksum += pm2_file->variable_0432;
		new_checksum += pm2_file->variable_0433;
		new_checksum += pm2_file->variable_0434;
		new_checksum += pm2_file->variable_0435;
		new_checksum += pm2_file->variable_0436;
		new_checksum += pm2_file->variable_0437;
		new_checksum += pm2_file->variable_0438;
		new_checksum += pm2_file->variable_0439;
		new_checksum += pm2_file->variable_0440;
		new_checksum += pm2_file->variable_0441;
		new_checksum += pm2_file->variable_0442;
		new_checksum += pm2_file->variable_0443;
		new_checksum += pm2_file->variable_0444;
		new_checksum += pm2_file->variable_0445;
		new_checksum += pm2_file->variable_0446;
		new_checksum += pm2_file->variable_0447;
		new_checksum += pm2_file->variable_0448;
		new_checksum += pm2_file->variable_0449;
		new_checksum += pm2_file->variable_0450;
		new_checksum += pm2_file->variable_0451;
		new_checksum += pm2_file->variable_0452;
		new_checksum += pm2_file->variable_0453;
		new_checksum += pm2_file->variable_0454;
		new_checksum += pm2_file->variable_0455;
		new_checksum += pm2_file->variable_0456;
		new_checksum += pm2_file->variable_0457;
		new_checksum += pm2_file->variable_0458;
		new_checksum += pm2_file->variable_0459;
		new_checksum += pm2_file->variable_0460;
		new_checksum += pm2_file->variable_0461;
		new_checksum += pm2_file->variable_0462;
		new_checksum += pm2_file->variable_0463;
		new_checksum += pm2_file->variable_0464;
		new_checksum += pm2_file->variable_0465;
		new_checksum += pm2_file->variable_0466;
		new_checksum += pm2_file->variable_0467;
		new_checksum += pm2_file->variable_0468;
		new_checksum += pm2_file->variable_0469;
		new_checksum += pm2_file->variable_0470;
		new_checksum += pm2_file->variable_0471;
		new_checksum += pm2_file->variable_0472;
		new_checksum += pm2_file->variable_0473;
		new_checksum += pm2_file->variable_0474;
		new_checksum += pm2_file->variable_0475;
		new_checksum += pm2_file->variable_0476;
		new_checksum += pm2_file->variable_0477;
		new_checksum += pm2_file->variable_0478;
		new_checksum += pm2_file->variable_0479;
		new_checksum += pm2_file->variable_0480;
		new_checksum += pm2_file->variable_0481;
		new_checksum += pm2_file->variable_0482;
		new_checksum += pm2_file->variable_0483;
		new_checksum += pm2_file->variable_0484;
		new_checksum += pm2_file->variable_0485;
		new_checksum += pm2_file->variable_0486;
		new_checksum += pm2_file->variable_0487;
		new_checksum += pm2_file->variable_0488;
		new_checksum += pm2_file->variable_0489;
		new_checksum += pm2_file->variable_0490;
		new_checksum += pm2_file->variable_0491;
		new_checksum += pm2_file->variable_0492;
		new_checksum += pm2_file->variable_0493;
		new_checksum += pm2_file->variable_0494;
		new_checksum += pm2_file->variable_0495;
		new_checksum += pm2_file->variable_0496;
		new_checksum += pm2_file->variable_0497;
		new_checksum += pm2_file->variable_0498;
		new_checksum += pm2_file->variable_0499;
		new_checksum += pm2_file->variable_0500;
		new_checksum += pm2_file->variable_0501;
		new_checksum += pm2_file->variable_0502;
		new_checksum += pm2_file->variable_0503;
		new_checksum += pm2_file->variable_0504;
		new_checksum += pm2_file->variable_0505;
		new_checksum += pm2_file->variable_0506;
		new_checksum += pm2_file->variable_0507;
		new_checksum += pm2_file->variable_0508;
		new_checksum += pm2_file->variable_0509;
		new_checksum += pm2_file->variable_0510;

		new_checksum += pm2_file->variable_0511;
		new_checksum += pm2_file->variable_0512;
		new_checksum += pm2_file->variable_0513;
		new_checksum += pm2_file->variable_0514;
		new_checksum += pm2_file->variable_0515;
		new_checksum += pm2_file->variable_0516;
		new_checksum += pm2_file->variable_0517;
		new_checksum += pm2_file->variable_0518;
		new_checksum += pm2_file->variable_0519;
		new_checksum += pm2_file->variable_0520;
		new_checksum += pm2_file->variable_0521;
		new_checksum += pm2_file->variable_0522;
		new_checksum += pm2_file->variable_0523;
		new_checksum += pm2_file->variable_0524;
		new_checksum += pm2_file->variable_0525;
		new_checksum += pm2_file->variable_0526;
		new_checksum += pm2_file->variable_0527;
		new_checksum += pm2_file->variable_0528;
		new_checksum += pm2_file->variable_0529;
		new_checksum += pm2_file->variable_0530;




		for (i = 0; i < 48; i++) {

			new_checksum += pm2_file->variable_0531[i];

		}



		for (i = 0; i < 512; i++) {

			new_checksum += pm2_file->variable_0536[i];

		}

		new_checksum += pm2_file->variable_0532;
		new_checksum += pm2_file->variable_0533;
		new_checksum += pm2_file->variable_0534;
		new_checksum += pm2_file->variable_0535;

		new_checksum += pm2_file->variable_0537;
		new_checksum += pm2_file->variable_0538;
		new_checksum += pm2_file->variable_0539;
		new_checksum += pm2_file->variable_0540;
		new_checksum += pm2_file->variable_0541;
		new_checksum += pm2_file->variable_0542;
		new_checksum += pm2_file->variable_0543;
		new_checksum += pm2_file->variable_0544;
		new_checksum += pm2_file->variable_0545;
		new_checksum += pm2_file->variable_0546;
		new_checksum += pm2_file->variable_0547;
		new_checksum += pm2_file->variable_0548;
		new_checksum += pm2_file->variable_0549;
		new_checksum += pm2_file->variable_0550;
		new_checksum += pm2_file->variable_0551;
		new_checksum += pm2_file->variable_0552;
		new_checksum += pm2_file->variable_0553;
		new_checksum += pm2_file->variable_0554;
		new_checksum += pm2_file->variable_0555;
		new_checksum += pm2_file->variable_0556;
		new_checksum += pm2_file->variable_0557;
		new_checksum += pm2_file->variable_0558;
		new_checksum += pm2_file->variable_0559;
		new_checksum += pm2_file->variable_0560;
		new_checksum += pm2_file->variable_0561;

		return new_checksum;

}