using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using GamePotUnity;

namespace GamePotSample
{
    public class Main : MonoBehaviour, IGamePot
    {
        [SerializeField]
        private GameObject mainUI;

        [SerializeField]
        private GameObject settingUI;

        [SerializeField]
        private GameObject purchaseUI;

        [SerializeField]
        private GameObject couponUI;

        [SerializeField]
        private GameObject gPGUI;

        [SerializeField]
        private GameObject googleButton;

        [SerializeField]
        private Text googleButtonText;

        [SerializeField]
        private GameObject facebookButton;

        [SerializeField]
        private Text facebookButtonText;

        [SerializeField]
        private GameObject lineButton;

        [SerializeField]
        private Text lineButtonText;

        [SerializeField]
        private GameObject twitterButton;

        [SerializeField]
        private Text twitterButtonText;


        [SerializeField]
        private Image pushButton;

        [SerializeField]
        private Text pushButtonText;

        [SerializeField]
        private Image nightPushButton;

        [SerializeField]
        private Text nightPushButtonText;

        [SerializeField]
        private Text memberId;

        [SerializeField]
        private InputField couponEdit;

        [SerializeField]
        private InputField unlockAchievementEdit;

        [SerializeField]
        private InputField incrementAchievementEdit1;

        [SerializeField]
        private InputField incrementAchievementEdit2;

        [SerializeField]
        private InputField submitLeaderboardEdit1;

        [SerializeField]
        private InputField submitLeaderboardEdit2;



        [SerializeField]
        private GameObject popupRoot;

        private Color active = new Color(14f / 255f, 247f / 255f, 38f / 255f);
        private Color dimmed = new Color(182f / 255f, 178f / 255f, 166f / 255f);

        public GamePotSendLogCharacter test_charlog = new GamePotSendLogCharacter();

        private void Start()
        {
            GamePot.setListener(this);

            mainUI.SetActive(true);
            settingUI.SetActive(false);
            purchaseUI.SetActive(false);
            couponUI.SetActive(false);

            linkingStatusUpdate();
            pushStatusUpdate();

            memberId.text = "회원번호 : " + GamePot.getMemberId();

            GamePot.d("d", "log d");
            GamePot.i("i", "log i");
            GamePot.w("w", "log w");
            GamePot.e("e", "log e");
        }

        private void linkingStatusUpdate()
        {
            List<NLinkingInfo> linkedList = GamePot.getLinkedList();

            googleButtonText.text = "Google 연결안됨";
            facebookButtonText.text = "Facebook 연결안됨";
            lineButtonText.text = "Line 연결안됨";
            twitterButtonText.text = "Twitter 연결안됨";


            if (linkedList != null)
            {
                foreach (NLinkingInfo item in linkedList)
                {
                    if (item.provider == NCommon.LinkingType.GOOGLE)
                    {
                        googleButtonText.text = "Google 연결됨";
                    }
                    else if (item.provider == NCommon.LinkingType.FACEBOOK)
                    {
                        facebookButtonText.text = "Facebook 연결됨";
                    }
                    else if (item.provider == NCommon.LinkingType.LINE)
                    {
                        lineButtonText.text = "Line 연결됨";
                    }
                    else if (item.provider == NCommon.LinkingType.TWITTER)
                    {
                        twitterButtonText.text = "Twitter 연결됨";
                    }
                }
            }
        }

        private bool getLinkingStatus(NCommon.LinkingType linkingType)
        {
            List<NLinkingInfo> linkedList = GamePot.getLinkedList();

            if (linkedList != null)
            {
                foreach (NLinkingInfo item in linkedList)
                {
                    if (linkingType == item.provider)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void pushStatusUpdate()
        {
            if (GamePot.getPushStatus().enable == true)
            {
                pushButtonText.text = "푸시 ON";
                pushButton.color = active;
            }
            else
            {
                pushButtonText.text = "푸시 OFF";
                pushButton.color = dimmed;
            }

            if (GamePot.getPushStatus().night == true)
            {
                nightPushButtonText.text = "야간푸시 ON";
                nightPushButton.color = active;
            }
            else
            {
                nightPushButtonText.text = "야간푸시 OFF";
                nightPushButton.color = dimmed;
            }
        }

#region UIButton.onClick
        public void ClickSettingsButton()
        {
            mainUI.SetActive(false);
            settingUI.SetActive(true);
            purchaseUI.SetActive(false);
            couponUI.SetActive(false);
            gPGUI.SetActive(false);
        }

        public void ClickSettingExitButton()
        {
            mainUI.SetActive(true);
            settingUI.SetActive(false);
            purchaseUI.SetActive(false);
            couponUI.SetActive(false);
            gPGUI.SetActive(false);
        }

        public void ClickCouponUIButton()
        {
            mainUI.SetActive(false);
            settingUI.SetActive(false);
            purchaseUI.SetActive(false);
            couponUI.SetActive(true);
            gPGUI.SetActive(false);
        }

        public void ClickNoticeButton()
        {
            // GamePot.showNoticeWebView();
            GamePot.showNotice(true);
            // GamePot.showWebView("http://27.96.130.104:8081/testbed/webbridge", 
            // (string result)=> {
            //     GamePot.d("moondory77 ", "showWebviewClose : " + result);
            // });
        }

        public void ClickCouponButton()
        {
            ClickCouponUIButton();
        }

        public void ClickCouponSubmitButton()
        {
            GamePot.coupon(couponEdit.text);
        }

        public void ClickInAppButton()
        {
            mainUI.SetActive(false);
            settingUI.SetActive(false);
            purchaseUI.SetActive(true);
            gPGUI.SetActive(false);
        }

        public void ClickGPG()
        {
            mainUI.SetActive(false);
            settingUI.SetActive(false);
            purchaseUI.SetActive(false);
            gPGUI.SetActive(true);
        }

        public void ClickShowCustomWebView(InputField input)
        {
            GamePot.showWebView(input.text);
        }

        public void ClickInAppItem1()
        {
#if !UNITY_EDITOR && UNITY_IOS
        GamePot.purchase("gamepot001");
#elif !UNITY_EDITOR && UNITY_ANDROID
        GamePot.purchase("purchase_001");
#elif UNITY_EDITOR
#endif
            // GamePot.purchaseThirdPayments("purchase_001");
        }

        public void ClickInAppItem2()
        {
#if !UNITY_EDITOR && UNITY_IOS
        GamePot.purchase("gamepot001");
#elif !UNITY_EDITOR && UNITY_ANDROID
        GamePot.purchase("purchase_002");
#elif UNITY_EDITOR
#endif
        }

        public void ClickInAppItem3()
        {
            var r = new System.Random();
            string tmp = (r.Next(100000, 999999) * 1000).ToString();

#if !UNITY_EDITOR && UNITY_IOS
            GamePot.sendPurchaseByThirdPartySDK("purchase_001", tmp, "KRW", 1000,  "apple", "apple", "purchaseBy3rdPartySDK");
#elif !UNITY_EDITOR && UNITY_ANDROID
              GamePot.sendPurchaseByThirdPartySDK("purchase_001", tmp, "KRW", 1200, "google", "google", "purchaseBy3rdPartySDK");
#endif
        }

        public void ClickCSButton()
        {
            // GamePot.showCSWebView();
            GamePot.showFaq();
        }

        public void ClickLinkingGoogleButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.GOOGLE) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.GOOGLE);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.GOOGLE);
            }
        }

        public void ClickLinkingFacebookButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.FACEBOOK) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.FACEBOOK);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.FACEBOOK);
            }
        }

        public void ClickLinkingLineButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.LINE) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.LINE);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.LINE);
            }
        }

        public void ClickLinkingTwitterButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.TWITTER) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.TWITTER);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.TWITTER);
            }
        }

        public void ClickPushButton()
        {
            GamePot.setPushStatus(!GamePot.getPushStatus().enable);
        }

        public void ClickNightPushButton()
        {
            GamePot.setPushNightStatus(!GamePot.getPushStatus().night);
        }

        public void ClickLogoutButton()
        {
            GamePot.logout();
        }

        public void ClickWithDrawnButton()
        {
            GamePot.deleteMember();
        }

        public void ClickLocalPush()
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "로컬 Push", "확인 버튼을 누르면 10초 후 Push 메세지가 수신 됩니다.", "확인", () =>
            {
                DateTime current = DateTime.Now;
                current = current.AddSeconds(10);
                GamePot.sendLocalPush(current, "title", "content");
            }, "취소", () => { });
        }

        public void ClickTerms()
        {
            GamePot.showTerms();
        }

        public void ClickPrivacy()
        {
            GamePot.showPrivacy();
        }

        public void ClickShowAchivement()
        {
            GamePot.showAchievement();
        }

        public void ClickShowLeaderboard()
        {
            GamePot.showLeaderboard();
        }

        public void ClickUnlockAchievement()
        {
            GamePot.unlockAchievement(unlockAchievementEdit.text);
        }

        public void ClickIncrementAchievement()
        {
            GamePot.incrementAchievement(incrementAchievementEdit1.text, incrementAchievementEdit2.text);
        }

        public void ClickSubmitLeaderboard()
        {
            GamePot.submitScoreLeaderboard(submitLeaderboardEdit1.text, submitLeaderboardEdit2.text);
        }

        public void ClickLoadAchievements()
        {
            GamePot.loadAchievement();
        }
#endregion

        // GamePot Interface
        public void onAppClose()
        {
            Application.Quit();
        }

        public void onNeedUpdate(NAppStatus status)
        {
        }

        public void onMainternance(NAppStatus status)
        {
        }

        public void onLoginCancel()
        {
        }

        public void onLoginSuccess(NUserInfo userInfo)
        {
        }

        public void onLoginFailure(NError error)
        {
        }

        public void onDeleteMemberSuccess()
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "회원탈퇴", "회원탈퇴 되었습니다.\n로그인 화면으로 이동 합니다.", "확인", () => { SceneManager.LoadSceneAsync("login"); });
        }

        public void onDeleteMemberFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "회원탈퇴", error.message, "확인", () => { });
        }

        public void onLogoutSuccess()
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "로그아웃", "로그아웃 되었습니다.\n로그인 화면으로 이동 합니다.", "확인", () => { SceneManager.LoadSceneAsync("login"); });
        }

        public void onLogoutFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "로그아웃", error.message, "확인", () => { });
        }

        public void onCouponSuccess()
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "쿠폰", "쿠폰 아이템이 지급되었습니다.", "확인", () => { });
        }

        public void onCouponFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "쿠폰", error.message, "확인", () => { });
        }

        public void onPurchaseSuccess(NPurchaseInfo purchaseInfo)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "결제", "결제 성공하였습니다.", "확인", () => { });
        }

        public void onPurchaseFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "결제", error.message, "확인", () => { });
        }

        public void onPurchaseCancel()
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "결제", "결제를 취소 하였습니다.", "확인", () => { });
        }

        public void onCreateLinkingSuccess(NUserInfo userInfo)
        {
            linkingStatusUpdate();
        }

        public void onCreateLinkingFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "연동", error.message, "확인", () => { });
            linkingStatusUpdate();
        }

        public void onCreateLinkingCancel()
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "연동", "연동이 취소 되었습니다.", "확인", () => { });
            linkingStatusUpdate();
        }

        public void onDeleteLinkingSuccess()
        {
            linkingStatusUpdate();
        }

        public void onDeleteLinkingFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "연동", error.message, "확인", () => { });
            linkingStatusUpdate();
        }

        public void onPushSuccess()
        {
            pushStatusUpdate();
        }

        public void onPushFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "Push", error.message, "확인", () => { });
            pushStatusUpdate();
        }

        public void onPushNightSuccess()
        {
            pushStatusUpdate();
        }

        public void onPushNightFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "Push Night", error.message, "확인", () => { });
            pushStatusUpdate();
        }

        public void onPushAdSuccess()
        {
            pushStatusUpdate();
        }

        public void onPushAdFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "Push AD", error.message, "확인", () => { });
            pushStatusUpdate();
        }

        public void onPushStatusSuccess()
        {
            pushStatusUpdate();
        }

        public void onPushStatusFailure(NError error)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "Push Status", error.message, "확인", () => { });
            pushStatusUpdate();
        }

        public void onAgreeDialogSuccess(NAgreeResultInfo info)
        {
        }

        public void onAgreeDialogFailure(NError error)
        {
        }

        public void onReceiveScheme(string scheme)
        {
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "Scheme", scheme, "확인", () => { });
        }

        public void onLoadAchievementSuccess(List<NAchievementInfo> info)
        {
            string msg = "";

            for (int i = 0; i < info.Count; i++)
            {
                msg += info[i].id + ", " + info[i].unlocked + "\n";
            }
            PopupManager.ShowCommonPopup(popupRoot, PopupManager.CommonPopupType.SMALL_SIZE, "Achievement", msg, "확인", () => { });
        }

        public void onLoadAchievementFailure(NError error)
        {
        }

        public void onLoadAchievementCancel()
        {
        }

        public void onWebviewClose(string result)
        {
        }
    }
}