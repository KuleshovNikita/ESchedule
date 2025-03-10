import React, { useEffect } from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AppRoutes from './components/routing/AppRoutes';
import Header from './components/markups/header/Header';
import {I18nextProvider} from "react-i18next";
import i18next from "i18next";
import Loader from './components/hoc/loading/Loader';
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
    return <Loader></Loader>;
  }

  return (
    <BrowserRouter>
      <React.StrictMode>
        <I18nextProvider i18n={i18next}>
          <Header /> 
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
