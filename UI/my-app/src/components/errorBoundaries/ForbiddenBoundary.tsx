import { AxiosError } from "axios";
import React, { Component, ErrorInfo, ReactNode } from "react";

interface Props {
    children?: ReactNode;
}
  
interface State {
    error: any;
}

class ForbiddenErrorBoundary extends Component<Props, State> {
    private promiseRejectionHandler = (event: PromiseRejectionEvent) => {
        this.setState({
            error: event.reason
        });
    }
  
    public state: State = {
        error: null
    };

    public static getDerivedStateFromError(error: Error): State {
        // Update state so the next render will show the fallback UI.
        return { error: error };
    }

    public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error("Uncaught error:", error, errorInfo);
    }

    componentDidMount() {
        // Add an event listener to the window to catch unhandled promise rejections & stash the error in the state
        window.addEventListener('unhandledrejection', this.promiseRejectionHandler)
    }

    componentWillUnmount() {
        window.removeEventListener('unhandledrejection', this.promiseRejectionHandler);
    }
  
    public render() {
      if (this.state.error && typeof this.state.error == typeof AxiosError) {
        if(this.state.error.response.status == 403) {
            return <h3>You have no right to access this page</h3>;
        }        
      } else {
        console.log(this.props)
        return this.props.children;
      }
    }
  }
  
  export default ForbiddenErrorBoundary;