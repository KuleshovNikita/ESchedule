import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';

import i18next from 'i18next';
import common_ua from "./translations/ua/common.json";
import common_en from "./translations/en/common.json";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

i18next.init({
  interpolation: { escapeValue: false },
  ns: 'common',
  lng: 'en',
  resources: {
    en: { common: common_en },
    ua: { common: common_ua },
  }
});

root.render(
  //<React.StrictMode>
    <App />
  //</React.StrictMode>
);

