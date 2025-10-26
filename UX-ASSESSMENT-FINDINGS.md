# Mead Calculator - UX/Usability Assessment Report

**Date:** October 26, 2025
**URL:** https://meadcalc-frontend-821052837153.us-central1.run.app
**Assessment Method:** Playwright automated testing + manual UX analysis

---

## Executive Summary

The Mead Calculator is a functional application with good real-time calculation features. However, there are several UX and accessibility opportunities for improvement. The assessment identified issues with accessibility compliance, form labeling, mobile usability, and information hierarchy that should be prioritized.

**Key Metrics:**
- ✓ Page loads successfully (3.6s desktop, 0.7s mobile)
- ⚠ 0 HTML labels detected (should have proper label associations)
- ⚠ Mobile buttons below WCAG minimum size (44x44px)
- ⚠ Unclear information hierarchy on first visit
- ⚠ No visible feedback for successful calculations

---

## Critical Findings

### 1. **Missing Form Labels (Accessibility Issue)** 🔴 HIGH PRIORITY

**Problem:** The assessment detected 0 HTML `<label>` elements associated with form inputs.

**Impact:**
- Screen reader users cannot understand what each input does
- Keyboard navigation is less effective
- WCAG 2.1 Level A violation (1.3.1 Info and Relationships)
- Mobile users cannot tap on labels to focus inputs

**Current State:**
```
❌ <input type="number" placeholder="0">
```

**Recommended Change:**
```
✓ <label htmlFor="honey-amount">Amount (g)</label>
  <input id="honey-amount" type="number" placeholder="0">
```

**Action Items:**
- [ ] Add `<label>` elements for all form inputs
- [ ] Use `htmlFor` attribute to link labels to input IDs
- [ ] Add visible labels (not just placeholders)
- [ ] Test with screen readers (NVDA, JAWS)

---

### 2. **Mobile Button Accessibility** 🔴 HIGH PRIORITY

**Problem:** 1 button found below WCAG minimum size of 44x44 pixels.

**Impact:**
- Difficult for users with motor control issues to tap
- Violates WCAG 2.1 Level AAA guideline (2.5.5)
- Higher error rates on mobile devices

**Recommended Change:**
- All interactive elements should be minimum 44x44 pixels
- Increase button padding from current values to meet minimum
- Add spacing between adjacent buttons

```css
/* Current (likely) */
button { padding: 8px 12px; }

/* Recommended */
button {
  padding: 12px 16px;
  min-height: 44px;
  min-width: 44px;
}
```

---

### 3. **Form Feedback & Validation** 🟡 MEDIUM PRIORITY

**Problem:** No visible feedback when calculations complete or errors occur.

**Impact:**
- Users unsure if their input was accepted
- Real-time calculations may feel unresponsive
- Error messages not obvious to users
- No success indicators

**Recommendations:**
1. **Success Indicators**
   - Add checkmark icon when honey/volume/ABV entered
   - Highlight results section with subtle animation
   - Show "Updated" timestamp in results

2. **Error Handling**
   - Display inline error messages near inputs
   - Use color-coded feedback (red for error, green for success)
   - Add error icons with explanatory tooltips

3. **Loading States**
   - Show loading spinner during API calls
   - Disable inputs during calculation
   - Display "Calculating..." status

---

### 4. **Mobile Layout & Scrolling** 🟡 MEDIUM PRIORITY

**Problem:** Page height is 1654px on mobile - requires significant scrolling to see results.

**Current Mobile Experience:**
1. User sees honey selector (scroll to fill)
2. User scrolls down to volume/ABV inputs
3. User scrolls down to see results
4. User scrolls down to add ingredients
5. User may need to scroll back up to change honey

**Recommendations:**
1. **Sticky Header**
   - Keep honey selector and navigation visible at top
   - Allows quick adjustments while viewing results

2. **Tab-Based Layout**
   - Tab 1: Quick Calculator (honey only + volume/ABV)
   - Tab 2: Advanced (with ingredients)
   - Reduces scrolling for simple use cases

3. **Bottom Sheet for Results**
   - Show results in fixed-position panel at bottom
   - Swipe up to expand, drag down to collapse
   - Keeps context visible while showing details

---

### 5. **Information Hierarchy** 🟡 MEDIUM PRIORITY

**Problem:** First-time users may not understand workflow or what values to enter.

**Current Issues:**
- Page title "Master the Art of Mead Making" doesn't explain calculator purpose
- No onboarding or help text
- Unclear which fields are required
- No visual distinction between honey selector and other inputs
- Results section appears but is often off-screen

**Recommendations:**
1. **Clearer Page Intro**
   ```
   "Create Your Mead Recipe"

   Select your honey type and amount, then specify your
   desired volume or ABV percentage. The calculator will
   determine the exact water needed.
   ```

2. **Required Field Indicators**
   - Add asterisks to required fields
   - Use subtle background color for required section
   - Label section: "Essential (Required)" vs "Additional (Optional)"

3. **Contextual Help**
   - Tooltip on hover over section headers
   - "Learn more" links for unfamiliar terms
   - Example values shown as hints

4. **Visual Grouping**
   - Use distinct background colors for each section
   - Add section headers: "1. Select Honey", "2. Target Recipe", "3. Results"
   - Number the steps for clarity

---

### 6. **Responsive Design Issues** 🟡 MEDIUM PRIORITY

**Problem:** Layout may not be optimal across all screen sizes.

**Findings:**
- ✓ Grid layout detected (good use of responsive classes)
- ⚠ Page appears to stack vertically on mobile (1654px height)
- ⚠ Some text may be too small on mobile

**Recommendations:**
1. **Font Sizing**
   - Minimum font size: 16px on mobile (prevents auto-zoom)
   - Use responsive sizing: `clamp(14px, 3vw, 18px)`

2. **Touch Targets**
   - Dropdowns/selects: min 44x44px
   - Input fields: min 48px tall
   - Spacing between interactive elements: min 8px

3. **Viewport Meta Tag**
   - Ensure `viewport` meta tag is present and correct
   - Should be: `<meta name="viewport" content="width=device-width, initial-scale=1">`

---

### 7. **Color Contrast** 🟡 MEDIUM PRIORITY

**Problem:** Specific color contrast values not measured, but should be verified.

**Recommendations:**
1. **Contrast Ratio Testing**
   - Verify all text has minimum WCAG AA contrast (4.5:1 for normal text)
   - Test with https://www.tpgi.com/color-contrast-checker/
   - Especially check: green background (#20752b) with text colors

2. **Dark Green Background (#20752b)**
   - Verify white text provides sufficient contrast
   - Check subtitle (gray-200) has adequate contrast
   - Test link colors on green background

3. **Color Blindness Considerations**
   - Don't rely on color alone for status (e.g., red for error)
   - Combine color with icons/text (✓ ✗)
   - Test with color blindness simulator

---

## Minor Findings

### 8. **Input Placeholder vs Label Confusion**

**Issue:** Relying on placeholders instead of visible labels.

**Problems:**
- Placeholders disappear when typing
- Screen readers may not announce placeholders
- Mobile autocomplete doesn't work well without labels

**Fix:** Add both visible labels AND placeholders
```jsx
<label htmlFor="honey-amount">Honey Amount (grams)</label>
<input
  id="honey-amount"
  type="number"
  placeholder="e.g., 500"
/>
```

---

### 9. **Missing Aria Attributes**

**Issue:** No ARIA attributes detected for complex interactions.

**Missing Attributes:**
- `aria-label` on icon-only buttons
- `aria-invalid` and `aria-describedby` on error states
- `aria-live` regions for real-time updates
- `aria-busy` during calculations

**Recommendations:**
```jsx
<button aria-label="Open honey menu">☰</button>

<input
  aria-invalid={hasError}
  aria-describedby={hasError ? "error-message" : undefined}
/>
<span id="error-message">Please select a honey type</span>

<div aria-live="polite" aria-busy={isCalculating}>
  {isCalculating ? "Calculating..." : "Ready"}
</div>
```

---

### 10. **No Keyboard Navigation Indicators**

**Issue:** Focus states may not be visible for keyboard users.

**Fix:**
```css
:focus {
  outline: 3px solid #4A90E2;
  outline-offset: 2px;
}

/* For button focus */
button:focus {
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.5);
}
```

---

## Recommendations Summary

### High Priority (Do First)
1. ✅ **Add HTML labels to all form inputs** - Critical for accessibility
2. ✅ **Ensure all buttons are 44x44px minimum** - WCAG compliance
3. ✅ **Add form validation and error messages** - Improves user confidence

### Medium Priority (Do Next)
4. 📊 **Improve information hierarchy** - Helps first-time users
5. 📱 **Optimize mobile layout** - Reduces scrolling, improves experience
6. ♿ **Verify color contrast ratios** - WCAG AA compliance
7. 🎯 **Add ARIA attributes** - Helps screen reader users

### Low Priority (Nice to Have)
8. 📍 **Add visual focus indicators** - Better keyboard navigation
9. 💡 **Add contextual help text** - Improves discoverability
10. 🎨 **Add success animations** - Better feedback

---

## Testing Checklist

### Manual Testing
- [ ] Test with keyboard-only navigation (no mouse)
- [ ] Test with screen reader (NVDA on Windows, VoiceOver on Mac)
- [ ] Test on real mobile devices (iPhone, Android)
- [ ] Test with browser zoom at 200%
- [ ] Test with Windows High Contrast mode enabled

### Automated Testing
- [ ] Run accessibility audit with axe DevTools
- [ ] Run Lighthouse audit
- [ ] Test color contrast with WebAIM Contrast Checker
- [ ] Test form inputs with automated testing

### User Testing
- [ ] Test with actual users (mead makers/newcomers)
- [ ] Observe where users click/scroll without guidance
- [ ] Ask users to complete tasks (create simple recipe)
- [ ] Collect feedback on clarity and usability

---

## Implementation Strategy

**Phase 1 (Week 1 - Critical):**
- Add HTML labels to all inputs
- Increase button sizes to 44x44px
- Add form validation with error messages

**Phase 2 (Week 2 - Important):**
- Add ARIA attributes
- Improve information hierarchy with section headers
- Verify color contrast and fix if needed
- Add loading/success states

**Phase 3 (Week 3 - Polish):**
- Optimize mobile layout (sticky header or tabs)
- Add keyboard focus indicators
- Add contextual help tooltips
- Enhanced animations for feedback

---

## Tools & Resources

**Accessibility Testing:**
- 🔗 [axe DevTools Browser Extension](https://www.deque.com/axe/devtools/)
- 🔗 [WAVE Web Accessibility Evaluation Tool](https://wave.webaim.org/)
- 🔗 [WebAIM Contrast Checker](https://webaim.org/resources/contrastchecker/)
- 🔗 [NVDA Screen Reader](https://www.nvaccess.org/)

**Performance Testing:**
- 🔗 [Google Lighthouse](https://developers.google.com/web/tools/lighthouse)
- 🔗 [WebPageTest](https://www.webpagetest.org/)

**Design & UX:**
- 🔗 [WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)
- 🔗 [ARIA Authoring Practices Guide](https://www.w3.org/WAI/ARIA/apg/)

---

## Conclusion

The Mead Calculator has solid functionality and good real-time calculation features. By implementing these UX and accessibility improvements, the application will be:

- ✅ More accessible to users with disabilities (WCAG compliant)
- ✅ Easier to use on mobile devices
- ✅ Clearer for first-time users
- ✅ More confident interactions with better feedback
- ✅ Better keyboard navigation support

The high-priority items (labels, button sizes, form feedback) should be implemented first, as they directly impact usability and accessibility compliance.
