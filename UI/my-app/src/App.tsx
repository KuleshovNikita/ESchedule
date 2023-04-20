import { useEffect } from 'react';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { useStore } from './api/stores/StoresManager';
import LoadingComponent from './components/hoc/loading/LoadingComponent';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { observer } from 'mobx-react-lite';
import AppRoutes from './components/AppRoutes';
import Header from './components/markups/Header/Header';
import AppLoader from './AppLoader';
import {I18nextProvider} from "react-i18next";
import i18next from "i18next";

function App() {
  const stores = useStore();
  
  useEffect(() => {
    AppLoader(stores);
  }, [stores]);

  if (!stores.commonStore.appLoaded) {
    return <LoadingComponent />;
  }

  return (
    <BrowserRouter>
      <React.StrictMode>
        <I18nextProvider i18n={i18next}>
          <Header />        
          <main className="main">
            <AppRoutes />
          </main>
          <ToastContainer position={"bottom-right"} 
                          limit={3} 
                          autoClose={5000} 
                          hideProgressBar={false}/>
        </I18nextProvider>
      </React.StrictMode>
    </BrowserRouter>
  );
}

export default observer(App);
