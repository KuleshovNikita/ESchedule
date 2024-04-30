import React, { useEffect } from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AppRoutes from './components/AppRoutes';
import Header from './components/markups/Header/Header';
import {I18nextProvider} from "react-i18next";
import i18next from "i18next";
import GlobalLoader from './components/hoc/loading/GlobalLoader';
import { useStore } from './api/stores/StoresManager';
import AppLoader from './AppLoader';
import { observer } from 'mobx-react-lite';
import ForbiddenErrorBoundary from './components/errorBoundaries/ForbiddenBoundary';

function App() {
  const stores = useStore();
  
  useEffect(() => {
    AppLoader(stores);
  }, []);

  if (!stores.commonStore.appLoaded) {
    return <GlobalLoader />;
  }

  return (
    <BrowserRouter>
      <React.StrictMode>
        <I18nextProvider i18n={i18next}>
          <Header /> 
          <GlobalLoader/>    
          <ForbiddenErrorBoundary> 
            <main className="main">
              <AppRoutes />
            </main>
          </ForbiddenErrorBoundary>  
          <ToastContainer position={"bottom-right"} 
                          limit={3} 
                          autoClose={3000} 
                          hideProgressBar={false}/>
        </I18nextProvider>
      </React.StrictMode>
    </BrowserRouter>
  );
}

export default observer(App);
