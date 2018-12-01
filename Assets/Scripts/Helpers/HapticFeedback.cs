using System.Runtime.InteropServices;

namespace NeonPlay {

	public static class HapticFeedback {

		public static bool enabled = true;

		public enum ImpactStyle {
			Light,
			Medium,
			Heavy
		}

		public enum NotificationType {
			Success,
			Warning,
			Error
		}

		#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		extern static private void HapticFeedbackImpactVibrate(int style);
		[DllImport("__Internal")]
		extern static private void HapticFeedbackNotificationVibrate(int type);
		[DllImport("__Internal")]
		extern static private void HapticFeedbackSelectionVibrate();
		#endif

		public static bool Available {
			get {
				#if UNITY_IOS
				// There's no way to test if Haptic is available and enabled on the device
				return true;
				#else
				return false;
				#endif
			}
		}

		public static void ImpactVibrate(ImpactStyle style) {

			if (enabled) {
			
				#if UNITY_IOS && !UNITY_EDITOR
				HapticFeedbackImpactVibrate((int) style);
				#endif
			}
		}

		public static void NotificationVibrate(NotificationType type) {

			if (enabled) {

				#if UNITY_IOS && !UNITY_EDITOR
				HapticFeedbackNotificationVibrate((int) type);
				#endif
			}
		}

		public static void SelectionVibrate() {

			if (enabled) {

				#if UNITY_IOS && !UNITY_EDITOR
				HapticFeedbackSelectionVibrate();
				#endif
			}
		}
	}
}
