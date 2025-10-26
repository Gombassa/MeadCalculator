const { chromium } = require('playwright');
const fs = require('fs');
const path = require('path');

const BASE_URL = 'https://meadcalc-frontend-821052837153.us-central1.run.app';
const SCREENSHOTS_DIR = './assessment-screenshots';
const REPORT_FILE = './assessment-report.json';

// Create screenshots directory
if (!fs.existsSync(SCREENSHOTS_DIR)) {
  fs.mkdirSync(SCREENSHOTS_DIR, { recursive: true });
}

const assessment = {
  timestamp: new Date().toISOString(),
  url: BASE_URL,
  findings: {
    pageLoad: {},
    accessibility: {},
    usability: {},
    ux: {},
    performance: {},
    responsive: {},
    interactions: {},
    errors: []
  },
  screenshots: [],
  recommendations: []
};

async function runAssessment() {
  const browser = await chromium.launch();
  const context = await browser.newContext();

  try {
    console.log('Starting Mead Calculator UX Assessment...\n');

    // Desktop viewport
    await testDesktopViewport(context);

    // Tablet viewport
    await testTabletViewport(context);

    // Mobile viewport
    await testMobileViewport(context);

    // Save report
    fs.writeFileSync(REPORT_FILE, JSON.stringify(assessment, null, 2));
    console.log('\nAssessment complete!');
    console.log('✓ Report saved to:', REPORT_FILE);
    console.log('✓ Screenshots saved to:', SCREENSHOTS_DIR);

  } catch (error) {
    assessment.findings.errors.push({
      type: 'ASSESSMENT_ERROR',
      message: error.message
    });
    console.error('Assessment error:', error.message);
  } finally {
    await browser.close();
  }
}

async function testDesktopViewport(context) {
  console.log('📱 Testing Desktop Viewport (1920x1080)...');
  const page = await context.newPage({
    viewport: { width: 1920, height: 1080 }
  });

  const errors = [];
  page.on('pageerror', error => {
    errors.push(error.message);
  });

  const startTime = Date.now();
  await page.goto(BASE_URL, { waitUntil: 'networkidle' });
  const loadTime = Date.now() - startTime;

  assessment.findings.pageLoad.desktop = {
    loadTime: `${loadTime}ms`,
    status: 'success'
  };

  console.log(`  ✓ Page loaded in ${loadTime}ms`);

  // Screenshot initial state
  const initialScreenshot = path.join(SCREENSHOTS_DIR, '1-desktop-initial.png');
  await page.screenshot({ path: initialScreenshot, fullPage: true });
  assessment.screenshots.push({
    name: 'Desktop - Initial State',
    file: initialScreenshot,
    viewport: 'desktop 1920x1080'
  });
  console.log('  ✓ Initial screenshot captured');

  // Check for UI elements
  const heading = await page.locator('h1').textContent();
  const tabButtons = await page.locator('button').count();

  assessment.findings.usability.desktop = {
    pageTitle: heading,
    navigationButtons: tabButtons,
    hasHoneySection: await page.locator('text=Honey Selection').count() > 0,
    hasVolumeInput: await page.locator('text=Desired Volume').count() > 0,
    hasABVInput: await page.locator('text=Desired ABV').count() > 0
  };

  // Test honey selection
  const honeySelects = await page.locator('select').count();
  if (honeySelects > 0) {
    console.log('  ✓ Found honey selector');
    const honeySelect = page.locator('select').first();
    await honeySelect.click();
    await page.waitForTimeout(300);

    const selectScreenshot = path.join(SCREENSHOTS_DIR, '2-desktop-dropdown.png');
    await page.screenshot({ path: selectScreenshot });
    assessment.screenshots.push({
      name: 'Desktop - Honey Dropdown Open',
      file: selectScreenshot
    });
  }

  // Test form input
  const numberInputs = await page.locator('input[type="number"]').count();
  if (numberInputs > 0) {
    console.log('  ✓ Found numeric inputs, testing real-time calculation...');
    const firstInput = page.locator('input[type="number"]').first();
    await firstInput.fill('500');
    await page.waitForTimeout(1000);

    const resultScreenshot = path.join(SCREENSHOTS_DIR, '3-desktop-results.png');
    await page.screenshot({ path: resultScreenshot, fullPage: true });
    assessment.screenshots.push({
      name: 'Desktop - With Values Entered',
      file: resultScreenshot
    });
  }

  // Check accessibility
  const labels = await page.locator('label').count();
  assessment.findings.accessibility.desktop = {
    labelCount: labels,
    errorMessages: errors,
    hasAlt: await page.locator('img[alt]').count()
  };

  console.log(`  ✓ Found ${labels} labels`);

  await page.close();
}

async function testTabletViewport(context) {
  console.log('\n📱 Testing Tablet Viewport (768x1024)...');
  const page = await context.newPage({
    viewport: { width: 768, height: 1024 }
  });

  const startTime = Date.now();
  await page.goto(BASE_URL, { waitUntil: 'networkidle' });
  const loadTime = Date.now() - startTime;

  assessment.findings.pageLoad.tablet = {
    loadTime: `${loadTime}ms`
  };

  console.log(`  ✓ Page loaded in ${loadTime}ms`);

  const tabletScreenshot = path.join(SCREENSHOTS_DIR, '4-tablet-initial.png');
  await page.screenshot({ path: tabletScreenshot, fullPage: true });
  assessment.screenshots.push({
    name: 'Tablet - Initial State',
    file: tabletScreenshot,
    viewport: 'tablet 768x1024'
  });
  console.log('  ✓ Tablet screenshot captured');

  // Check layout responsiveness
  const inputs = await page.locator('input').count();
  assessment.findings.responsive.tablet = {
    inputCount: inputs,
    columnsDetected: await page.locator('[class*="col"]').count() > 0
  };

  await page.close();
}

async function testMobileViewport(context) {
  console.log('\n📱 Testing Mobile Viewport (375x667)...');
  const page = await context.newPage({
    viewport: { width: 375, height: 667 }
  });

  const startTime = Date.now();
  await page.goto(BASE_URL, { waitUntil: 'networkidle' });
  const loadTime = Date.now() - startTime;

  assessment.findings.pageLoad.mobile = {
    loadTime: `${loadTime}ms`
  };

  console.log(`  ✓ Page loaded in ${loadTime}ms`);

  const mobileScreenshot = path.join(SCREENSHOTS_DIR, '5-mobile-initial.png');
  await page.screenshot({ path: mobileScreenshot, fullPage: true });
  assessment.screenshots.push({
    name: 'Mobile - Initial State',
    file: mobileScreenshot,
    viewport: 'mobile 375x667'
  });
  console.log('  ✓ Mobile screenshot captured');

  // Check button sizes for accessibility
  const buttons = await page.locator('button').all();
  let smallButtons = 0;
  for (const button of buttons) {
    try {
      const box = await button.boundingBox();
      if (box && (box.width < 44 || box.height < 44)) {
        smallButtons++;
      }
    } catch (e) {}
  }
  assessment.findings.usability.mobileButtonsUnderSize = smallButtons;

  // Test full scroll
  const scrollHeight = await page.evaluate(() => document.body.scrollHeight);
  await page.evaluate(() => window.scrollTo(0, document.body.scrollHeight));
  await page.waitForTimeout(500);

  const mobileFullScreenshot = path.join(SCREENSHOTS_DIR, '6-mobile-scrolled.png');
  await page.screenshot({ path: mobileFullScreenshot, fullPage: true });
  assessment.screenshots.push({
    name: 'Mobile - After Scrolling',
    file: mobileFullScreenshot
  });

  console.log(`  ✓ Page height: ${scrollHeight}px`);
  console.log(`  ✓ Found ${buttons.length} buttons`);
  console.log(`  ⚠ ${smallButtons} buttons below 44x44px (WCAG guideline)`);

  await page.close();
}

// Run assessment
runAssessment().catch(console.error);
