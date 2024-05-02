import PageBox from "../../components/wrappers/PageBox";
import { useCult } from "../../hooks/Translator";

const styles = require('./NotFound.module.css');

export default function NotFound() {
    const { translator } = useCult();

    return(
        <PageBox>
            <div className={styles.textStyle}>
                {translator('messages.page-not-found')}
            </div>
        </PageBox>
    );
}