extern "C"  {

	void HapticFeedbackImpactVibrate(int style) {
	
		UIImpactFeedbackGenerator *feedbackGenerator = [[UIImpactFeedbackGenerator alloc] init];
		
		[feedbackGenerator initWithStyle:(UIImpactFeedbackStyle) style];
		[feedbackGenerator impactOccurred];

		feedbackGenerator = NULL;
	}
	
	void HapticFeedbackNotificationVibrate(int type) {

		UINotificationFeedbackGenerator *feedbackGenerator = [[UINotificationFeedbackGenerator alloc] init];
		
		[feedbackGenerator notificationOccurred:(UINotificationFeedbackType) type];

		feedbackGenerator = NULL;
	}

	void HapticFeedbackSelectionVibrate() {

		UISelectionFeedbackGenerator *feedbackGenerator = [[UISelectionFeedbackGenerator alloc] init];
		
		[feedbackGenerator selectionChanged];

		feedbackGenerator = NULL;
	}
}
