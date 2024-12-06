/**
* @fileOverview JavaScript Initialisation Library.
* @author <a href="https://github.com/richardhenyash">Richard Ash</a>
* @version 1.1.1
*/
/*jshint esversion: 6 */

// Buffer balloon sprite images //
const imgBalloonBlue = new Image();

imgBalloonBlue.src = "../img/Practice3/balloon-blue-sprite.png";
const imgBalloonOrange = new Image();
imgBalloonOrange.src = "../img/Practice3/balloon-orange-sprite.png";
const imgBalloonPurple = new Image();
imgBalloonPurple.src = "../img/Practice3/balloon-purple-sprite.png";
const imgBalloonRed = new Image();
imgBalloonRed.src = "../img/Practice3/balloon-red-sprite.png";
const imgBalloonGreen = new Image();
imgBalloonGreen.src = "../img/Practice3/balloon-green-sprite.png";
const imgBalloonYellow = new Image();
imgBalloonYellow.src = "../img/Practice3/balloon-yellow-sprite.png";

// Buffer sound effects, set volume to 0.6 //
const soundPop = new Audio("../img/Practice3/sounds/pop.mp3");
soundPop.volume = 0.5;
const soundDeflate = new Audio("../img/Practice3/sounds/deflate.mp3");
soundDeflate.volume = 0.5;
const soundHighScore = new Audio("../img/Practice3/sounds/high-score.mp3");
soundHighScore.volume = 0.5;
const soundUnlucky = new Audio("../img/Practice3/sounds/unlucky.mp3");
soundUnlucky.volume = 0.5;
const soundWellDone = new Audio("../img/Practice3/sounds/well-done.mp3");
soundWellDone.volume = 0.5;

// Initialise global variables //
let bpmSoundEffectsMuted = false;
let bpmGameMode;
let bpmQno;
let bpmDifficulty;
let bpmActiveButtons;
let bpmOptionArray;
let bpmQArray;
let bpmHealthArray;
let bpmCQ;
let bpmQCurrent;
let bpmAnswerArray;
let bpmScoreArray;